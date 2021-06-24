﻿using System.Collections.ObjectModel;

namespace Illumina.BaseSpace.SDK
{
	public interface IClientSettings
	{
		uint RetryAttempts { get; }
		
		Collection<int> RetryableCodes { get; }

		string BaseSpaceWebsiteUrl { get; }

		string BaseSpaceApiUrl { get; } 

        string BaseSpaceBillingApiUrl { get; set; }

		string Version { get; }

        uint FileUploadMultipartChunkSize { get; }
        uint FileUploadMultipartSizeThreshold { get; }

        uint FileDownloadMultipartSizeThreshold { get; }

		IAuthentication Authentication { get; }

        int TimeoutMin { get;  }
	}
}
