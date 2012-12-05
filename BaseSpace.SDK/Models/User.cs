using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Illumina.BaseSpace.SDK.Models
{
    [DataContract( Name = "User")]
    public class UserCompact : AbstractResource
    {
        [DataMember(IsRequired = true)]
        public override string Id { get; set; }

        [DataMember]
        public override Uri Href { get; set; }

        [DataMember]
        public string Name { get; set; }
    }

    [DataContract()]
    public class User : UserCompact
    {
        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string GravatarUrl { get; set; }

        [DataMember]
        public DateTime? DateLastActive { get; set; }

        [DataMember]
        public DateTime DateCreated { get; set; }

        [DataMember]
        public Uri HrefRuns { get; set; }

        [DataMember]
        public Uri HrefProjects { get; set; }
    }
}
