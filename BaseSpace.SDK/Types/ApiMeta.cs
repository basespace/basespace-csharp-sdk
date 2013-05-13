using System;
using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract(Name = "Meta")]
    public class ApiMeta
    {
        [DataMember]
        public string Build { get; set; }

        [DataMember]
        public Uri HrefRuns { get; set; }
        
        [DataMember]
        public string HrefCurrentUser { get; set; }
        
        [DataMember]
        public string HrefOAuthAuthorizeDialog { get; set; }

        [DataMember]
        public string HrefApplications { get; set; }

        [DataMember]
        public string HrefApiDocumentation { get; set; }

        [DataMember]
        public string HrefProjects { get; set; }
    }
}

