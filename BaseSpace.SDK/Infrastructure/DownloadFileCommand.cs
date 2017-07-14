using System;
using System.IO;
using System.Net;
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

        public DownloadFileCommand(BaseSpaceClient client, V1pre3FileCompact file, Stream stream, IClientSettings settings, CancellationToken token = new CancellationToken(), IWebProxy proxy = null, bool enableLogging = true): this(client, file.Id, stream, settings, token, proxy, enableLogging)
        {
          
        }

        public DownloadFileCommand(BaseSpaceClient client, string fileId, Stream stream, IClientSettings settings, CancellationToken token = new CancellationToken(), IWebProxy proxy = null, bool enableLogging = true)
        {
            DateTime expiration;
            string url = GetFileContentUrl(client,fileId, out expiration);
#pragma warning disable 618
            ILargeFileDownloadParameters parameters = new LargeFileDownloadWithStreamParameters(new Uri(url), stream, 0, id: fileId, maxThreads: DEFAULT_THREADS, maxChunkSize: (int)settings.FileDownloadMultipartSizeThreshold, autoCloseStream: false, verifyLength: true);
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
            string url = GetFileContentUrl(client, file.Id, out expiration);
            ILargeFileDownloadParameters parameters = new LargeFileDownloadParameters(new Uri(url), targetFileName, maxThreads: threadCount, maxChunkSize: (int?)settings.FileDownloadMultipartSizeThreshold, id: file.Id);
            _parameters = parameters;
            _token = token;
            _enableLogging = enableLogging;
        }

        public DownloadFileCommand(BaseSpaceClient client, V1pre3FileCompact file, string targetFileName, IClientSettings settings, int threadCount,  int maxChunkSize, CancellationToken token = new CancellationToken(), bool enableLogging = true)
        {
            DateTime expiration;
            string url = GetFileContentUrl(client, file.Id, out expiration);
            _fileName = string.Format("[{0}],{1}",file.Id,file.Name);
            ILargeFileDownloadParameters parameters = new LargeFileDownloadParameters(new Uri(url), targetFileName, maxThreads: threadCount, maxChunkSize: maxChunkSize, id: file.Id);
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
			if (FileDownloadProgressChanged != null)
			{
				FileDownloadProgressChanged(this, e);
			}
		}

		private static string GetFileContentUrl(BaseSpaceClient client, string fileId, out DateTime expiration)
		{
			// get the download URL
			var response = client.GetFileContentUrl(new FileContentRedirectMetaRequest(fileId));

			if (response.Response == null || response.Response.HrefContent == null)
			{
				throw new ApplicationException("Unable to get HrefContent");
			}

			if (!response.Response.SupportsRange)
			{
				throw new ApplicationException("This file does not support range queries");
			}

			expiration = response.Response.Expires;
			return response.Response.HrefContent;
		}

	}
}

