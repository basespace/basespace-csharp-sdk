namespace Illumina.BaseSpace.SDK
{
	public interface IClientSettings
	{
		uint RetryAttempts { get; }

		string BaseSpaceWebsiteUrl { get; }

		string BaseSpaceApiUrl { get; }

		string Version { get; }
		
		uint FileUploadMultipartSizeThreshold { get; }

        uint FileDownloadMultipartSizeThreshold { get; }

		IAuthentication Authentication { get; }
	}
}
