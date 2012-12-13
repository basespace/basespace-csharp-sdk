namespace Illumina.BaseSpace.SDK.Types
{
    public class RequestOptions : IRequestOptions
    {
       
        public uint RetryAttempts { get;  set; }

        public string AuthCode { get;  set; }

        public string BaseUrl { get;  set; }
    }
}
