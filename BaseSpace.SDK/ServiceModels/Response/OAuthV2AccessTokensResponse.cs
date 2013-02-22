using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    [DataContract()]
    public class OAuthV2AccessTokenResponse
    {
        [DataMember(Name="access_token")]
        public string AccessToken { get; set; }

        [DataMember(Name="expires_in")]
        public int? ExpiresIn { get; set; }
        
        [DataMember(Name="error")]
        public string Error { get; set; }
        
        [DataMember(Name="error_description")]
        public string ErrorMessage { get; set; }
        
        [DataMember(Name="error_uri")]
        public string ErrorUri { get; set; }
    }

}
