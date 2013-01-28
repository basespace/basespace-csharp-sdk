using System;
using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.Types
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

    [DataContract]
    public class AppResultFilesCompact : AbstractResource
    {
        [DataMember(IsRequired = true)]
        public override string Id { get; set; }

        [DataMember]
        public override Uri Href { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string ContentType { get; set; }

        [DataMember]
        public string Size { get; set; }

        [DataMember]
        public string Path { get; set; }

        [DataMember]
        public DateTime DateCreated { get; set; }

    }

    public enum AppResultSortByParameters { Id, Name, DateCreated }
}
