using System;
using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract(Name = "FileSet")]
    public class FileSet : AbstractResource
    {
        [DataMember(IsRequired = true)]
        public override string Id { get; set; }

        [DataMember(IsRequired = true)]
        public override Uri Href { get; set; }

        [DataMember(IsRequired = true)]
        public Uri HrefFiles { get; set; }
    }
    [DataContract(Name = "GenomeFileSet")]
    public class GenomeFileSet : FileSet
    {
        [DataMember]
        public string Category { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string OriginalPath { get; set; }
    }
    [DataContract(Name = "GenomeAnnotationFileSet")]
    public class GenomeAnnotationFileSet : FileSet
    {
        [DataMember]
        public string Source { get; set; }
        [DataMember]
        public string Version { get; set; }
        [DataMember]
        public string OriginalPath { get; set; }
    }

}
