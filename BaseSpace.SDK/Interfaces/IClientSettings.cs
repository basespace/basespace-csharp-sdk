﻿namespace Illumina.BaseSpace.SDK
{
	public interface IClientSettings
	{
		uint RetryAttempts { get; }

		string BaseSpaceWebsiteUrl { get; }

		string BaseSpaceApiUrl { get; } 

		string Version { get; }

        uint FileUploadMultipartChunkSize { get; }
        uint FileUploadMultipartSizeThreshold { get; }

        uint FileDownloadMultipartSizeThreshold { get; }

		IAuthentication Authentication { get; }

        int TimeoutMin { get;  }
        bool UseS3Proxy { get; }
    }
}
