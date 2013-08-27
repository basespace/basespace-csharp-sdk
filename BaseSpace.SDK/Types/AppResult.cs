using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.Types
{

    [DataContract( Name = "AppResult")]
    public class AppResultCompact : AbstractResource, IPropertyContent
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

        public string Type { get { return PropertyTypes.APPRESULT; } }

        public override string ToString()
        {
            return string.Format("Href: {0}; Name: {1}; Status: {2}", Href, Name, Status);
        }
    }

    [DataContract]
    public class AppResult : AppResultCompact, IPropertyContainingResource
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

        [DataMember]
        public PropertyContainer Properties { get; set; }
    }

    [DataContract]
    public class AppResultFileCompact : FileCompact
    {
    }

    public enum AppResultSortByParameters { Id, Name, DateCreated }
}
