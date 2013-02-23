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

		public virtual FileResponse UploadFile<T>(FileInfo fileToUpload, string resourceId, string resourceIdentifierInUri, string directory = null,
                                         int numThreads = 16)
			where T : FileUploadRequestBase<FileResponse>, new()
        {
            Logger.DebugFormat("numthreads {0}", numThreads);
            FileResponse file = null;

            RetryLogic.DoWithRetry(3, string.Format("Uploading file {0}", fileToUpload.Name),
                                   () =>
                                   {
                                       file = fileToUpload.Length >= ClientSettings.FileMultipartSizeThreshold ?
										   UploadFile_MultiPart<T>(fileToUpload, resourceId, resourceIdentifierInUri, directory, numThreads) :
										   UploadFile_SinglePart<T>(fileToUpload, resourceId, resourceIdentifierInUri, directory);
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

        protected virtual FileResponse UploadFile_SinglePart<T>(FileInfo fileToUpload, string resourceId, string resourceIdentifierInUri, string directory = null)
			where T : FileUploadRequestBase<FileResponse>, new()
        {
            var req = new T()
            {
                Id = resourceId,
                Name = fileToUpload.Name,
                MultiPart = false
            };

            if (directory != null)
                req.Directory = directory;

            var uri = string.Format("{0}/{1}/{2}/files", ClientSettings.Version, resourceIdentifierInUri, req.Id);

            var resp = WebClient.Send(req);
            return resp ?? null;
        }


        protected virtual FileResponse UploadFile_MultiPart<T>(FileInfo fileToUpload, string resourceId, string resourceIdentifierInUri, string directory, int numThreads)
			where T : FileUploadRequestBase<FileResponse>, new()
        {
            var req = new T()
            {
                Id = resourceId,
                Name = fileToUpload.Name,
                MultiPart = true
            };

            if (directory != null)
                req.Directory = directory;

            //var uri = string.Format("{0}/{1}/{2}/files", ClientSettings.Version, resourceIdentifierInUri, req.Id);

            Logger.InfoFormat("File Upload: {0}: Initiating multipart upload", fileToUpload.Name);

            var fileUploadresp = WebClient.Send<FileResponse>(req);

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
            var zeroBasedPartNumberMax = (fileToUpload.Length - 1) / ClientSettings.FileMultipartSizeThreshold;
            var success = false;

            // shared signal to let all threads know if one part failed
            // if so, other threads shouldn't bother uploading
            var errorSignal = new ManualResetEvent(false);
            var sync = new object();
            var totalPartsUploaded = 0;

            Parallel.For(0, (int)(1 + zeroBasedPartNumberMax),
                         new ParallelOptions() { MaxDegreeOfParallelism = numThreads },
                         new Action<int>(zeroBasedPartNumber =>
                         {
                             var partNumber = zeroBasedPartNumber + 1;
                             var byteOffset = zeroBasedPartNumber * ClientSettings.FileMultipartSizeThreshold;
                             var serviceUrl = string.Format("{0}/{1}/files/{2}/parts/{3}", ClientSettings.BaseSpaceApiUrl.TrimEnd('/'),ClientSettings.Version, fileId, partNumber);
                             Logger.DebugFormat("Uploading part {0}/{1} of {2}", partNumber, 1 + zeroBasedPartNumberMax, fileToUpload.FullName);
                             
                             UploadPart(serviceUrl, fileToUpload, byteOffset, zeroBasedPartNumber, errorSignal, string.Format("{0}/{1}", partNumber, zeroBasedPartNumberMax + 1));
                             lock (sync)
                             {
                                 totalPartsUploaded++;
                                 Logger.DebugFormat("Done Uploading part {0}/{1} of {2}, {3} total parts uploaded",
                                                    partNumber, 1 + zeroBasedPartNumberMax, fileToUpload.FullName,
                                                    totalPartsUploaded);
                             }
                         }));
            success = errorSignal.WaitOne(0) == false;  //timeout occurs, means was not set, means success
           
            var status = success ? FileUploadStatus.complete : FileUploadStatus.aborted;

            var statusReq = new FileRequestPost()
            {
                UploadStatus = status
            };

            //var uri2 = string.Format("{0}/files/{1}", ClientSettings.Version, fileId);
            var response = WebClient.Send(statusReq);

            Logger.InfoFormat("File Upload: {0}: Finished with status {1}", fileToUpload.FullName, status);

            return response;
        }


        static object _syncRead = new object();
        
        protected virtual void UploadPart(string fullUrl, FileInfo fileToUpload, long startPosition, int partNumber, ManualResetEvent errorSignal, string partDescription)
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
                            data = BufferPool.GetChunk(Convert.ToInt32(ClientSettings.FileMultipartSizeThreshold));

	                        var authentication = Options.Authentication as OAuth2Authentication;
	                        authentication.UpdateHttpHeader(wc.Headers);

                            int actualSize;
                            int desiredSize = (int)Math.Min(fileToUpload.Length - startPosition, Convert.ToInt32(ClientSettings.FileMultipartSizeThreshold));
                            lock (_syncRead) // avoid thrashing the disk
                                using (var fs = System.IO.File.Open(fileToUpload.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
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

		private class BSWebClient : WebClient
		{
			//TODO: Doesnt seem right? Need refactor?
			// const int CONNECTION_LIMIT = 16;
			protected override WebRequest GetWebRequest(Uri address)
			{
				var request = base.GetWebRequest(address) as HttpWebRequest;

				if (request != null && request.ServicePoint.ConnectionLimit < 20)
					request.ServicePoint.ConnectionLimit = 10000;  //Note: Is this changing global value?

				return request;
			}
		}
    }
}
