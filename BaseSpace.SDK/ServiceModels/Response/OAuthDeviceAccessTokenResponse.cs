using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    [DataContract]
    public class OAuthDeviceAccessTokenResponse
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }

        [DataMember(Name = "error")]
        public string Error { get; set; }

        [DataMember(Name = "error_description")]
        public string ErrorMessage { get; set; }
    }
}

