using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract(Name = "Coverage")]
    public class Coverage
    {
        [DataMember(IsRequired = true)]
        public string Id { get; set; }

        [DataMember]
        public long[] MeanCoverage { get; set; }
        [DataMember]
        public string Chrom { get; set; }
        
        [DataMember]
        public long StartPos { get; set; }
        
        [DataMember]
        public long EndPos { get; set; }
        
        [DataMember]
        public int BucketSize { get; set; }
    }

    [DataContract(Name = "CoverageMeta")]
    public class CoverageMeta
    {
        [DataMember(IsRequired = true)]
        public string Id { get; set; }
        
        [DataMember]
        public long MaxCoverage { get; set; }

        [DataMember]
        public int CoverageGranularity { get; set; }
    }
}
