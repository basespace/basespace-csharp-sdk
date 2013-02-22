using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels.Response
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
