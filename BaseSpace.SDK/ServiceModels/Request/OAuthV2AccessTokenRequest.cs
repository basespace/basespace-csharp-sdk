using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    [DataContract]
    public class OAuthV2AccessTokenRequest : AbstractRequest<OAuthV2AccessTokenResponse>
    {
        public OAuthV2AccessTokenRequest(string clientId, string clientSecret, string redirectUri, string authorizationCode)
        {
            grant_type = "authorization_code";
            client_id = clientId;
            client_secret = clientSecret;
            redirect_uri = redirectUri;
            code = authorizationCode;
			HttpMethod = HttpMethods.POST;
        }

        //[DataMember(Name= "grant_type")]
        public string grant_type { get; set; }

        //[DataMember(Name = "code")]
        public string code { get; set; }

        //[DataMember(Name = "redirect_uri")]
        public string redirect_uri { get; set; }

        //[DataMember(Name = "client_id")]
        public string client_id { get; set; }

        //[DataMember(Name = "client_secret")]
        public string client_secret { get; set; }

		protected override string GetUrl()
		{
			return string.Format("{0}/oauthv2/token", Version);
		}
	}

}
