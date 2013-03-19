using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK
{
	internal class DownloadFileCommand
	{
		private const int CONNECTION_COUNT = 16; //TODO: Is this the right place?

		private readonly BaseSpaceClient _client;
	    private readonly IClientSettings _settings;
		private readonly Stream _stream;
        private readonly FileCompact _file;
	    private readonly IWebProxy _proxy;
	    private static CancellationToken Token { get; set; }
        private int ChunkSize { get; set; }
        private int MaxRetries { get; set; }

        public DownloadFileCommand(BaseSpaceClient client, FileCompact file, Stream stream, IClientSettings settings, CancellationToken token = new CancellationToken(), IWebProxy proxy = null)
			: this(client, stream, settings, token, proxy)
        {
            _file = file;
        }

        public DownloadFileCommand(BaseSpaceClient client, string fileId, Stream stream, IClientSettings settings, CancellationToken token = new CancellationToken(), IWebProxy proxy = null)
			: this(client, stream, settings, token, proxy)
        {
			_file = _client.GetFilesInformation(new GetFileInformationRequest(fileId)).Response;
        }

		private DownloadFileCommand(BaseSpaceClient client, Stream stream, IClientSettings settings, CancellationToken token, IWebProxy proxy)
		{
			_client = client;
			_settings = settings;
			_stream = stream;
		    _proxy = proxy;
			Token = token;
			ChunkSize = Convert.ToInt32(_settings.FileDownloadMultipartSizeThreshold);
			MaxRetries = Convert.ToInt32(_settings.RetryAttempts);
		}

		public event FileDownloadProgressChangedEventHandler FileDownloadProgressChanged;

		public void Execute()
		{

            string contentUrl = null;
            var urlExpiration = DateTime.Now;
            var sync = new object();

            var getUrl = new Func<string>(() =>
            {
                lock (sync)
                {
                    if (DateTime.Now > urlExpiration - TimeSpan.FromMinutes(10))
                    {
                        contentUrl = GetFileContentUrl(out urlExpiration);
                    }
                    return contentUrl;
                }
            });

            DownloadFile(getUrl, _stream, Convert.ToInt64(_file.Size), ChunkSize, UpdateStatusForFile, MaxRetries, LogManager.GetCurrentClassLogger(), _proxy);
		}

	    private void UpdateStatusForFile(int downloadedChunkCount, int totalChunkCount, int chunkSizeAdj, double span)
	    {
            OnFileDownloadProgressChanged(new FileDownloadProgressChangedEventArgs(
                                _file.Id,
                                100 * downloadedChunkCount / totalChunkCount,
                                8000.0 * chunkSizeAdj /
                                span));
	    }

	    public static void DownloadFile(Func<string> getUrl, Stream stream, long fileSize, int chunkSize,Action<int,int,int,double> updateStatus =null, int maxRetries = 3 , ILog Logger = null, IWebProxy proxy = null)
        {
            var parallelOptions = new ParallelOptions()
            {
                MaxDegreeOfParallelism = CONNECTION_COUNT,
                CancellationToken = Token
            };
            var totalChunkCount = GetChunkCount(fileSize, chunkSize);
            var sync = new object();
	        var downloadedChunkCount = 0;
            Action<int> downloadPart = (partNumber) =>
	            {
	                var startDateTime = DateTime.Now;
                    var startPosition = (long)partNumber * chunkSize;
                    var chunkSizeAdj = GetChunkSize(fileSize, chunkSize, partNumber);
                    var endPosition = startPosition + chunkSizeAdj - 1;

	                parallelOptions.CancellationToken.ThrowIfCancellationRequested();

	                GetByteRange(getUrl, startPosition, endPosition, (b, pos, len) =>
	                                                {
                                                        lock (sync)
                                                        {
                                                            stream.Position = pos;
                                                            stream.Write(b, 0, (int)len);
                                                        }
	                                                }, chunkSize, maxRetries, Logger, proxy);

	                lock (sync)
	                {
	                    downloadedChunkCount++;
	                }

                    if (updateStatus != null)
    	                updateStatus(downloadedChunkCount,totalChunkCount,chunkSizeAdj,DateTime.Now.Subtract(startDateTime).TotalMilliseconds);
	            };

            try
	        {
                int part = -1;
                Parallel.For(0, totalChunkCount, parallelOptions, (index) =>
                                                                    {
                                                                        int nextPart = Interlocked.Increment(ref part);
                                                                        downloadPart(nextPart);
                                                                    });
	        }
	        catch (OperationCanceledException)
	        {
	        }
	    }

	    protected virtual void OnFileDownloadProgressChanged(FileDownloadProgressChangedEventArgs e)
		{
			if (FileDownloadProgressChanged != null)
			{
				FileDownloadProgressChanged(this, e);
			}
		}


		private string GetFileContentUrl(out DateTime expiration)
		{
			// get the download URL
			var response = _client.GetFileContentUrl(new FileContentRedirectMetaRequest(_file.Id));

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

		private static int GetChunkCount(long fileSize, int chunkSize)
		{
            return (int)(fileSize / chunkSize + (fileSize % chunkSize > 0 ? 1 : 0));
		}

		private static int GetChunkSize(long fileSize, int chunkSize, int zeroBasedChunkNumber)
		{
			var chunkCount = GetChunkCount(fileSize, chunkSize);

			if (zeroBasedChunkNumber + 1 < chunkCount)
			{
				return chunkSize;
			}

			if (zeroBasedChunkNumber >= chunkCount)
			{
				return 0;
			}

            var remainder = (int)(fileSize % chunkSize);
			return remainder > 0 ? remainder : chunkSize;
		}

		private static void GetByteRange(Func<string> absoluteUrl, long start, long end, Action<byte[], long, long> dataHandler, int chunkSize, int maxRetries, ILog Logger, IWebProxy proxy)
		{
			var len = end - start + 1;
			if (len > chunkSize)
				throw new ArgumentOutOfRangeException("Byte range requested is too large");
			RetryLogic.DoWithRetry(Convert.ToUInt32(maxRetries), string.Format("GetByteRange {0} -> {1} from {2}", start, end, absoluteUrl()), // may not be the actual url used
				() =>
				{
					while (start < end + 1)
					{
						string url = absoluteUrl();
						var webreq = HttpWebRequest.Create(url) as HttpWebRequest;
						webreq.ServicePoint.ConnectionLimit = CONNECTION_COUNT;
						webreq.ServicePoint.UseNagleAlgorithm = true;
					    webreq.Proxy = proxy;

                        // not implemented on mono :(
                        //webreq.ServicePoint.ReceiveBufferSize = 1048576;  // GV: I experienced improvements using this setting and downloading on 10Gb instances and sockets
						
                        webreq.Timeout = 200000;
                        webreq.Proxy = null;

						Logger.InfoFormat("requesting {0}->{1}", start, end);
						webreq.AddRange(start, end);

						using (var resp = webreq.GetResponse() as HttpWebResponse)
							start += CopyResponse(start, dataHandler, resp, chunkSize,Logger);
					}
				},
			Logger);
		}

        private static int CopyResponse(long start, Action<byte[], long, long> dataHandler, WebResponse resp, int chunkSize, ILog Logger)
		{
			using (var stm = resp.GetResponseStream())
			{
				var buffer = BufferPool.GetChunk(chunkSize);

				int totalRead = 0;
				try
				{
					int read;
					var length = (int)resp.ContentLength;
					while ((read = stm.Read(buffer, totalRead, length - totalRead)) > 0)
						totalRead += read;

					dataHandler(buffer, start, totalRead);
				}
				finally
				{
					BufferPool.ReleaseChunk(buffer);
				}
				return totalRead;
			}
		}
	}
}

