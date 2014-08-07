namespace Illumina.BaseSpace.SDK.Types
{
    public class RequestOptions : IRequestOptions
    {
        public RequestOptions(uint retryAttempts = BaseSpaceClientSettings.DEFAULT_RETRY_ATTEMPTS)
        {
            RetryAttempts = retryAttempts;
        }

        public uint RetryAttempts { get; set; }
    }
}
