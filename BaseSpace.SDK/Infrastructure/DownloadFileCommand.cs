using System;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.Types;
using Illumina.TerminalVelocity;

namespace Illumina.BaseSpace.SDK
{
    internal class DownloadFileCommand : IAsyncProgress<LargeFileDownloadProgressChangedEventArgs>
	{
        private const int DEFAULT_THREADS = 16;
        private readonly ILargeFileDownloadParameters _parameters;
	    private readonly IWebProxy _proxy;
	    private CancellationToken _token { get; set; }
        private static ILog logger = LogManager.GetCurrentClassLogger();
        private bool _enableLogging = true;
        private string _fileName = "NotSet";
	    private static bool UseS3Proxy = false;
        public DownloadFileCommand(BaseSpaceClient client, V1pre3FileCompact file, Stream stream, IClientSettings settings, CancellationToken token = new CancellationToken(), IWebProxy proxy = null, bool enableLogging = true): this(client, file.Id, stream, settings, token, proxy, enableLogging)
        {
          
        }

        public DownloadFileCommand(BaseSpaceClient client, string fileId, Stream stream, IClientSettings settings, CancellationToken token = new CancellationToken(), IWebProxy proxy = null, bool enableLogging = true)
        {
            DateTime expiration;
            long fileSize;
            
            string url = GetFileContentUrl(client, fileId, out expiration, out fileSize);
#pragma warning disable 618
            ILargeFileDownloadParameters parameters = new LargeFileDownloadWithStreamParameters
                (
                 new Uri(url), stream, fileSize, id: fileId, maxThreads: DEFAULT_THREADS, maxChunkSize: (int)settings.FileDownloadMultipartSizeThreshold, 
                 autoCloseStream: false, verifyLength: false, accessToken: UseS3Proxy?settings.Authentication.AccessToken:""
                );
#pragma warning restore 618
            _parameters = parameters;
            _token = token;
            _proxy = proxy;
            _enableLogging = enableLogging;

        }

        public DownloadFileCommand(BaseSpaceClient client, V1pre3FileCompact file, string targetFileName,
                                   IClientSettings settings, CancellationToken token = new CancellationToken(), bool enableLogging = true, int threadCount = DEFAULT_THREADS)
        {
            DateTime expiration;
            long fileSize;
            string url = GetFileContentUrl(client, file.Id, out expiration, out fileSize);

            ILargeFileDownloadParameters parameters = new LargeFileDownloadParameters
                (
                    new Uri(url), targetFileName, fileSize: fileSize, maxThreads: threadCount, maxChunkSize: (int?)settings.FileDownloadMultipartSizeThreshold, 
                    id: file.Id, verifyLength: false, accessToken: UseS3Proxy ? settings.Authentication.AccessToken : ""
                );
            _parameters = parameters;
            _token = token;
            _enableLogging = enableLogging;
        }

        public DownloadFileCommand(BaseSpaceClient client, V1pre3FileCompact file, string targetFileName, IClientSettings settings, int threadCount,  int maxChunkSize, CancellationToken token = new CancellationToken(), bool enableLogging = true)
        {
            DateTime expiration;
            long fileSize;
            string url = GetFileContentUrl(client, file.Id, out expiration, out fileSize);
            _fileName = string.Format("[{0}],{1}",file.Id,file.Name);
            ILargeFileDownloadParameters parameters = new LargeFileDownloadParameters
                (
                    new Uri(url), targetFileName, fileSize: fileSize, maxThreads: threadCount, maxChunkSize: maxChunkSize, id: file.Id, 
                    verifyLength: false, accessToken: UseS3Proxy ? settings.Authentication.AccessToken : ""
                );
            _parameters = parameters;
            _token = token;
            _enableLogging = enableLogging;
        }

        public DownloadFileCommand(Uri uri, string targetFileName, string fileId = null, CancellationToken token = new CancellationToken(), bool enableLogging = true)
        {
            ILargeFileDownloadParameters parameters = new LargeFileDownloadParameters(uri, targetFileName, 0,
                                                                                      fileId ?? targetFileName);
            _parameters = parameters;
            _token = token;
            _enableLogging = enableLogging;

        }

        protected DownloadFileCommand(ILargeFileDownloadParameters downloadParameters,
                                   CancellationToken token = new CancellationToken(), IWebProxy proxy = null, bool enableLogging = true)
        {
            _token = token;
            _proxy = proxy;
            _parameters = downloadParameters;
            _enableLogging = enableLogging;
        }


		public event FileDownloadProgressChangedEventHandler FileDownloadProgressChanged;

		public void Execute()
		{
           // This is where we are using the new downloader code 
		    try
		    {
		        if (_enableLogging)
		        {
		            var task = _parameters.DownloadAsync(_token, this, s => logger.Debug(s));
		            task.Wait(_token);
		        }
		        else
		        {
		            var task = _parameters.DownloadAsync(_token, this);
		            task.Wait(_token);
		        }
		    }
		    catch (OperationCanceledException e)
		    {
                logger.DebugFormat("OperationCanceledException {0}", _fileName);
		        logger.Error(e);
		    }
            logger.DebugFormat("Execution Over with _token.IsCancellationRequested = {0} for {1}", _token.IsCancellationRequested, _fileName);
		}

        /// <summary>
        /// Get progress from download (Terminal Velocity)
        /// </summary>
        /// <param name="e"></param>
        public void Report(LargeFileDownloadProgressChangedEventArgs e)
        {
            OnFileDownloadProgressChanged(new FileDownloadProgressChangedEventArgs(_parameters.Id, e.ProgressPercentage, e.DownloadBitRate,e.BytesDownloaded, e.IsFailed));
		}
     
	    protected virtual void OnFileDownloadProgressChanged(FileDownloadProgressChangedEventArgs e)
        {
            FileDownloadProgressChanged?.Invoke(this, e);
        }
        private static string GetFileContentUrl(BaseSpaceClient client, string fileId, out DateTime expiration, out long fileSize)
        {
            if (UseS3Proxy)
            {
                return GetS3ProxiedFileContentUrl(client, fileId, out expiration, out fileSize);
            }
            // get the download URL
            var response = client.GetFileContentUrl(new FileContentRedirectMetaRequest(fileId));

            if (response.Response?.HrefContent == null)
            {
                throw new ApplicationException("Unable to get HrefContent");
            }

            if (!response.Response.SupportsRange)
            {
                throw new ApplicationException("This file does not support range queries");
            }

            expiration = response.Response.Expires;

            //We will try to get the filesize using the S3 url, if that fails, then that means
            //that access to S3 is blocked because of restrictive network policies, therefore
            //we will use the S3Proxied Url 
            try
            {
                fileSize = new Uri(response.Response.HrefContent).GetContentLength(maxRetries: 1);
            }           
            catch (Exception ex)
            {
                logger.ErrorFormat("Switching to S3Proxy mode because of {0}",ex,ex.Message);
                UseS3Proxy = true;
                return GetS3ProxiedFileContentUrl(client, fileId, out expiration, out fileSize);

            }
            return response.Response.HrefContent;
        }

	    private static string GetS3ProxiedFileContentUrl(BaseSpaceClient client, string fileId, out DateTime expiration, out long fileSize)
	    {
	        var infoResponse = client.GetFilesInformation(new GetFileInformationRequest(fileId));
	        if (infoResponse.Response?.HrefContent == null)
	        {
	            throw new ApplicationException("Unable to get HrefContent");
	        }
	        expiration = DateTime.MaxValue;

	        var apiUri = new Uri(client.Settings.BaseSpaceApiUrl);
	        var uriBuilder = new System.UriBuilder(apiUri);
	        uriBuilder.Path += infoResponse.Response.HrefContent;
	        uriBuilder.Query = "redirect=proxy";
	        fileSize = infoResponse.Response.Size;
	        return uriBuilder.Uri.ToString();
	    }
	}
}

