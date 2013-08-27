using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    [DataContract]
    public class OAuthV2AccessTokenRequest : AbstractRequest<OAuthV2AccessTokenResponse>
    {
        public OAuthV2AccessTokenRequest(string clientId, string clientSecret, string redirectUri, string authorizationCode)
        {
            GrantType = "authorization_code";
            ClientId = clientId;
            ClientSecret = clientSecret;
            RedirectUri = redirectUri;
            AuthorizationCode = authorizationCode;
			HttpMethod = HttpMethods.POST;
        }

        [DataMember(Name= "grant_type")]
        public string GrantType { get; set; }

        [DataMember(Name = "code")]
        public string AuthorizationCode { get; set; }

        [DataMember(Name = "redirect_uri")]
        public string RedirectUri { get; set; }

        [DataMember(Name = "client_id")]
        public string ClientId { get; set; }

        [DataMember(Name = "client_secret")]
        public string ClientSecret { get; set; }

		protected override string GetUrl()
		{
			return string.Format("{0}/oauthv2/token", Version);
		}
	}
}
