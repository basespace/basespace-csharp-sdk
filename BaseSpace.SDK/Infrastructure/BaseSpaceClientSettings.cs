namespace Illumina.BaseSpace.SDK
{
	public class BaseSpaceClientSettings : IClientSettings
	{
		public const uint DEFAULT_RETRY_ATTEMPTS = 6;

		public const string DEFAULT_WEBSITE = "https://basespace.illumina.com";

		public const string DEFAULT_API = "https://api.basespace.illumina.com";

		public const string DEFAULT_VERSION = "v1pre3";

        public const uint DEFAULT_UPLOAD_MULTIPART_SIZE = 20 * 1024 * 1024; //in bytes
        public const uint DEFAULT_DOWNLOAD_MULTIPART_SIZE = 1024 * 1024; //in bytes

		public const uint DEFAULT_MULTIPART_SIZE_THRESHOLD = 25*1024*1024; //in bytes

		public BaseSpaceClientSettings()
		{
			RetryAttempts = DEFAULT_RETRY_ATTEMPTS;
			BaseSpaceApiUrl = DEFAULT_API;
			BaseSpaceWebsiteUrl = DEFAULT_WEBSITE;
			Version = DEFAULT_VERSION;
            FileUploadMultipartSizeThreshold = DEFAULT_UPLOAD_MULTIPART_SIZE;
            FileDownloadMultipartSizeThreshold = DEFAULT_DOWNLOAD_MULTIPART_SIZE;
		}

		public uint RetryAttempts { get; set; }

		public string BaseSpaceWebsiteUrl { get; set; }

		public string BaseSpaceApiUrl { get; set; }

		public string Version { get; set; }

        public uint FileUploadMultipartSizeThreshold { get; set; }

        public uint FileDownloadMultipartSizeThreshold { get; set; }

        public IAuthentication Authentication { get; set; }

        public int TimeoutMin { get; set; }
	}
}
