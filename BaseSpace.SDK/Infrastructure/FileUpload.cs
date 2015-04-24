using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using Illumina.BaseSpace.SDK.ServiceModels;
using ServiceStack.ServiceClient.Web;
using Illumina.BaseSpace.SDK.Types;
using File = System.IO.File;

namespace Illumina.BaseSpace.SDK
{
    public class FileUpload
    {
        protected IWebClient WebClient { get; set; }
        protected IClientSettings ClientSettings { get; set; }
        protected IRequestOptions Options { get; set; }
        protected ILog Logger = LogManager.GetCurrentClassLogger();

        public FileUpload(IWebClient webClient, IClientSettings settings, IRequestOptions options)
        {
            WebClient = webClient;
            ClientSettings = settings;
            Options = options;
        }

        public virtual TResult UploadFile<TResult>(FileUploadRequestBase<TResult> request)
            where TResult : FileResponse
        {
            Logger.DebugFormat("numthreads {0}", request.ThreadCount);
            TResult file = null;

            RetryLogic.DoWithRetry(ClientSettings.RetryAttempts, string.Format("Uploading file {0}", request.FileInfo.Name),
                                   () =>
                                   {
                                       request.MultiPart = request.FileInfo.Length >= ClientSettings.FileUploadMultipartSizeThreshold;

                                       file = request.MultiPart.Value ?
                                           UploadFile_MultiPart(request) :
                                           WebClient.Send(request);
                                   }, Logger, retryHandler:
                                   (exc) =>
                                   {
                                       var appExc = exc as ApplicationException;
                                       if (appExc != null && appExc.InnerException != null)
                                       {
                                           var wse = appExc.InnerException as WebServiceException;
                                           if (wse != null)
                                               // we need to reupload the file with the conflict error
                                               if (wse.StatusCode == 409)
                                                   return true;
                                           return RetryLogic.GenericRetryHandler(wse);
                                       }
                                       return false;
                                   }
                                   );
            return file;
        }

        protected virtual TResult UploadFile_MultiPart<TResult>(FileUploadRequestBase<TResult> request)
            where TResult : FileResponse
        {
            Logger.InfoFormat("File Upload: {0}: Initiating multipart upload", request.FileInfo.Name);

            var fileUploadresp = WebClient.Send(request);

            var server = ClientSettings.BaseSpaceApiUrl.TrimEnd('/');

            uint chunkSize = ClientSettings.FileUploadMultipartChunkSize;

            if (fileUploadresp == null)
            {
                Logger.ErrorFormat("File Upload: Failed initiating upload");
                return null;
            }

            if (fileUploadresp.Response == null)
            {
                Logger.ErrorFormat("File Upload: {0}: Failed initiating upload", fileUploadresp.ResponseStatus.Message);
                return null;
            }

            var fileId = fileUploadresp.Response.Id;
            var zeroBasedPartNumberMax = (request.FileInfo.Length - 1) / chunkSize;

            if (zeroBasedPartNumberMax > 9999)
            {
                long l = request.FileInfo.Length;
                // use the good old division formula: N=Q*D+R where N,Q,D and R are integers and R=N%D ==> partnumber(zerobased)=Q=(N-N%D)/D
                // the trick is that R=N%D only makes sense for zero based numbers since it's a modulo so
                // N needs to be zero-based and then Q is zero based since N%D is also a zero-based number and D is one-based... pheww!
                var chunkSize10000Parts = (uint) (((l - 1) - (l - 1) % 10000) / 10000 + 1);
                uint newChunkSize = Math.Max(ClientSettings.FileDownloadMultipartSizeThreshold, chunkSize10000Parts);

                Logger.DebugFormat("the file you are trying to upload is too big ({0} bytes) for the part size you specified ({1} bytes) (there can be at most 10000 parts). the chunksize parameter will be overridden with {2} bytes", request.FileInfo.Length, ClientSettings.FileUploadMultipartChunkSize, newChunkSize);
                chunkSize = newChunkSize;
                zeroBasedPartNumberMax = (request.FileInfo.Length - 1) / chunkSize;
            }

			// shared signal to let all threads know if one part failed
            // if so, other threads shouldn't bother uploading
            var errorSignal = new ManualResetEvent(false);
            var sync = new object();
            var totalPartsUploaded = 0;

            Parallel.For(0, (int)(1 + zeroBasedPartNumberMax),
                         new ParallelOptions() { MaxDegreeOfParallelism = request.ThreadCount },
                         (zeroBasedPartNumber =>
                         {
                             var partNumber = zeroBasedPartNumber + 1;
                             var byteOffset = zeroBasedPartNumber * chunkSize;
                             var serviceUrl = string.Format("{0}/{1}/files/{2}/parts/{3}", server, ClientSettings.Version, fileId, partNumber);
                             Logger.DebugFormat("Uploading part {0}/{1} of {2}", partNumber, 1 + zeroBasedPartNumberMax, request.FileInfo.FullName);

                             UploadPart(serviceUrl, request.FileInfo, byteOffset, zeroBasedPartNumber, errorSignal, string.Format("{0}/{1}", partNumber, zeroBasedPartNumberMax + 1),chunkSize);
                             lock (sync)
                             {
                                 totalPartsUploaded++;
                                 Logger.DebugFormat("Done Uploading part {0}/{1} of {2}, {3} total parts uploaded",
                                                    partNumber, 1 + zeroBasedPartNumberMax, request.FileInfo.FullName,
                                                    totalPartsUploaded);
                             }
                         }));

            bool success = errorSignal.WaitOne(0) == false;

            var status = success ? FileUploadStatus.complete : FileUploadStatus.aborted;

            var statusReq = new FileRequestPost<TResult>
            {
                Id = fileId,
                UploadStatus = status
            };

            var response = WebClient.Send(statusReq);

            Logger.InfoFormat("File Upload: {0}: Finished with status {1}", request.FileInfo.FullName, status);

            return response;
        }


        static object _syncRead = new object();

        protected virtual void UploadPart(string fullUrl, FileInfo fileToUpload, long startPosition, int partNumber, ManualResetEvent errorSignal, string partDescription, uint chunkSize)
        {
            RetryLogic.DoWithRetry(ClientSettings.RetryAttempts, string.Format("Uploading part {0} of {1}", partDescription, fileToUpload.Name),
                () =>
                {
                    if (errorSignal.WaitOne(0))
                        return;

                    using (var wc = new BSWebClient())
                    {
                        byte[] data = null;
                        try
                        {
                            data = BufferPool.GetChunk((int)chunkSize);

                            var authentication = ClientSettings.Authentication;
                            authentication.UpdateHttpHeader(wc.Headers, new Uri(fullUrl), "PUT");

                            int actualSize;
                            int desiredSize = (int)Math.Min(fileToUpload.Length - startPosition, chunkSize);
                            lock (_syncRead) // avoid thrashing the disk
                                using (var fs = System.IO.File.Open(fileToUpload.FullName, FileMode.Open, FileAccess.Read, FileShare.Delete | FileShare.Read))
                                {
                                    fs.Seek(startPosition, SeekOrigin.Begin);
                                    actualSize = fs.Read(data, 0, desiredSize);
                                }
                            if (actualSize != desiredSize)
                            {
                                throw new Exception("we didn't read what we expected from this file");
                            }
                            if (actualSize != data.Length)
                                data = data.Take(actualSize).ToArray(); // TODO: Find a way to prevent creating sub-arrays (memory fragmentation)
                            Logger.InfoFormat("File Upload: {0}: Uploading part {1}", fileToUpload.Name, partDescription);
                            wc.UploadData(fullUrl, "PUT", data);

                        }
                        finally
                        {
                            BufferPool.ReleaseChunk(data);
                        }
                    }
                },
                    Logger,
                    error: () => errorSignal.Set());  // notify other threads to give up
        }

        public static int NumFileUploadParts(FileInfo file)
        {
            return (int)((file.Length - 1)/BaseSpaceClientSettings.DEFAULT_MULTIPART_SIZE_THRESHOLD + 1);
        }

        private class BSWebClient : WebClient
        {
            //TODO: Doesnt seem right? Need refactoring?
            // const int CONNECTION_LIMIT = 16;
            protected override WebRequest GetWebRequest(Uri address)
            {
                var request = base.GetWebRequest(address) as HttpWebRequest;

                if (request != null && request.ServicePoint.ConnectionLimit < 20)
                    request.ServicePoint.ConnectionLimit = 10000;  //Note: Is this changing global value?

                // Workaround to support slow internet connections.
                // On very slow internet connections (specifically between the client and the BaseSpace API server), 
                // multi-part file uploads tend to fail due to a request timeout.
                if (request != null)
                {
                    // Set the timeout to 10 min (vs default 100 sec). 
                    // This setting was confirmed to work for the minimum upload part size of 5 MB
                    // on connections with sustainable upload speeds of 20 KB/s and higher.
                    // Technically, this timeout should be tied to the configured part size, 
                    // but cursory tests demonstrated that for larger part sizes greater timeouts
                    // start inducing errors (500 in particular) on the server side.
                    request.Timeout = 600000;
                    request.ReadWriteTimeout = request.Timeout;
                }

                return request;
            }
        }
    }
}
