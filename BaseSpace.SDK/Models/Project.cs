using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Illumina.BaseSpace.SDK.Models
{
    [DataContract( Name = "Project")]
    public class ProjectCompact : AbstractResource
    {
        [DataMember(IsRequired = true)]
        public override string Id { get; set; }

        [DataMember]
        public UserCompact UserOwnedBy { get; set; }

        [DataMember]
        public override Uri Href { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public DateTime DateCreated { get; set; }
    }

    [DataContract()]
    public class Project : ProjectCompact
    {
        [DataMember]
        public Uri HrefSamples { get; set; }
        [DataMember]
        public Uri HrefAppResults { get; set; }
        [DataMember]
        public Uri HrefBaseSpaceUI { get; set; }
    }
}
