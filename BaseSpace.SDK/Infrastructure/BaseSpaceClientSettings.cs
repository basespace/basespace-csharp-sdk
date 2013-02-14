namespace Illumina.BaseSpace.SDK
{
	public class BaseSpaceClientSettings : IClientSettings
	{
		public const uint DEFAULT_RETRY_ATTEMPTS = 3;
		public const string DEFAULT_WEBSITE = "https://basespace.illumina.com";
		public const string DEFAULT_API = "https://api.basespace.illumina.com";
		public const string DEFAULT_VERSION = "v1pre3";
		public const uint DEFAULT_MULTIPART_SIZE = 20*1024*1024; //in bytes
		public const uint DEFAULT_MULTIPART_SIZE_THRESHOLD = 25*1024*1024; //in bytes

		public BaseSpaceClientSettings()
		{
			RetryAttempts = DEFAULT_RETRY_ATTEMPTS;
			BaseSpaceApiUrl = DEFAULT_API;
			BaseSpaceWebsiteUrl = DEFAULT_WEBSITE;
			Version = DEFAULT_VERSION;
			FileMultipartSizeThreshold = DEFAULT_MULTIPART_SIZE;
		}

		public uint RetryAttempts { get; set; }
		public string AppClientId { get; set; }
		public string AppClientSecret { get; set; }
		public string BaseSpaceWebsiteUrl { get; set; }
		public string BaseSpaceApiUrl { get; set; }
		public string AppRedirectUrl { get; set; }
		public string Version { get; set; }
		public string OauthAuthorizeUrl { get; set; }
		public uint FileMultipartSizeThreshold { get; set; }
		public IAuthentication Authentication { get; set; }
	}
}
