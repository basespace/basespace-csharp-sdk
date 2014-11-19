namespace Illumina.BaseSpace.SDK.Types
{
	public class RequestOptions : IRequestOptions
	{
		public RequestOptions(uint retryAttempts = BaseSpaceClientSettings.DEFAULT_RETRY_ATTEMPTS,
            double powerbase = BaseSpaceClientSettings.DEFAULT_RETRY_POWER_BASE)
		{
			RetryAttempts = retryAttempts;
		    RetryPowerBase = powerbase;
		}

		public uint RetryAttempts { get; set; }
        public double RetryPowerBase { get; set; }
    }
}
