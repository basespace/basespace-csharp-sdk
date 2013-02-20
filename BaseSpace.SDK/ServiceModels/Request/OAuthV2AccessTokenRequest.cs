using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Illumina.BaseSpace.SDK.ServiceModels.Request
{

    [DataContract]
    public class OAuthV2AccessTokenRequest
    {
        public OAuthV2AccessTokenRequest(string clientId, string clientSecret, string redirectUri, string authorizationCode)
        {
            GrantType = "authorization_code";
            ClientId = clientId;
            ClientSecret = clientSecret;
            RedirectUri = redirectUri;
            AuthorizationCode = authorizationCode;
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
    }

}
