using System;

namespace Illumina.BaseSpace.SDK.Types
{
	public class RequestOptions : IRequestOptions
	{
		public RequestOptions() :
			this(BaseSpaceClientSettings.DEFAULT_API)
		{
		}

		public RequestOptions(string baseUrl, uint retryAttempts = BaseSpaceClientSettings.DEFAULT_RETRY_ATTEMPTS)
		{
			if (string.IsNullOrWhiteSpace(baseUrl))
			{
				throw new ArgumentException("BaseUrl is required");
			}

			BaseUrl = baseUrl;
			RetryAttempts = retryAttempts;
		}

		public uint RetryAttempts { get; set; }

		public string BaseUrl { get; set; }
	}
}
