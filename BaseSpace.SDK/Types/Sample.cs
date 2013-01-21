using System;
using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.Types
{

    [DataContract( Name = "Sample")]
    public class SampleCompact : AbstractResource
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
        public string SampleId { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string StatusSummary { get; set; }

        [DataMember]
        public DateTime DateCreated { get; set; }

        [DataMember]
        public IContentReference<IAbstractResource>[] References { get; set; }
    }

    [DataContract()]
    public class Sample : SampleCompact
    {
        [DataMember]
        public Uri HrefGenome { get; set; }

        [DataMember]
        public int? SampleNumber { get; set; }

        [DataMember]
        public string ExperimentName { get; set; }

        [DataMember]
        public Uri HrefFiles { get; set; }

        [DataMember]
        public AppSessionCompact AppSession { get; set; }

        [DataMember]
        public bool IsPairedEnd { get; set; }
        [DataMember]
        public short Read1 { get; set; }
        [DataMember]
        public short Read2 { get; set; }
        [DataMember]
        public long NumReadsRaw { get; set; }
        [DataMember]
        public long NumReadsPF { get; set; }
    }

    [DataContract]
    public class SampleFilesCompact : AbstractResource
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
    
    public enum SamplesSortByParameters { Id, Name, DateCreated }
}
