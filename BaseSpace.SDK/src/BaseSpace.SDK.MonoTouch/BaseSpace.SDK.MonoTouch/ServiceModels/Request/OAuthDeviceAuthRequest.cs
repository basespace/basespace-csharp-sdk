using System;
using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    [DataContract]
    public class OAuthDeviceAuthRequest
    {
        public OAuthDeviceAuthRequest (string clientId, string scope)
        {
            ResponseType = "device_code";
            ClientId = clientId;
            Scope = scope;
        }

        [DataMember(Name = "response_type")]
        public string ResponseType { get; private set; }
        [DataMember(Name = "client_id")]
        public string ClientId { get; set; }
        [DataMember(Name = "scope")]
        public string Scope { get; set; }
    }
}

