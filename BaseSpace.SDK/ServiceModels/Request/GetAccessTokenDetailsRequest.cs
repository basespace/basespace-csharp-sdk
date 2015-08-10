

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class GetAccessTokenDetailsRequest : AbstractResourceRequest<GetAccessTokenDetailsResponse>
    {
        public string access_token { get; set; }

        protected override string GetUrl()
        {
            return string.Format("{0}/oauthv2/token/current", Version);
        }
    }
}
