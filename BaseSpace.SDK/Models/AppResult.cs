using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Illumina.BaseSpace.SDK.Models
{

    [DataContract( Name = "AppResult")]
    public class AppResultCompact : AbstractResource
    {
        [DataMember(IsRequired = true)]
        public override string Id { get; set; }

        [DataMember]
        public override Uri Href { get; set; }

        [DataMember]
        public UserCompact UserOwnedBy { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string StatusSummary { get; set; }

        [DataMember]
        public DateTime DateCreated { get; set; }

    }

    [DataContract]
    public class AppResult : AppResultCompact
    {
        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public Uri HrefFiles { get; set; }

        [DataMember]
        public Uri HrefGenome { get; set; }

        [DataMember]
        public AppSessionCompact AppSession { get; set; }

        [DataMember]
        public IContentReference<IAbstractResource>[] References { get; set; }
    }
}
