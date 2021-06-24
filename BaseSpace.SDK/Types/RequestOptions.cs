using System.Collections.ObjectModel;

namespace Illumina.BaseSpace.SDK.Types
{
	public class RequestOptions : IRequestOptions
	{
		public RequestOptions(uint retryAttempts = BaseSpaceClientSettings.DEFAULT_RETRY_ATTEMPTS,  Collection<int> retryableCodes = null)
		{
			RetryableCodes = retryableCodes ?? BaseSpaceClientSettings.DEFAULT_RETRY_STATUS_CODES;
			RetryAttempts = retryAttempts;
		}

		public uint RetryAttempts { get; set; }

		public Collection<int> RetryableCodes { get; set; }
	}
}
