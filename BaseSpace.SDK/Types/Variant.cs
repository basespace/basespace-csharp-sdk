using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract(Name = "VariantHeader")]
    public class VariantHeader : AbstractResource
    {
        [DataMember(IsRequired = true)]
        public override string Id { get; set; }

        [DataMember]
        public override Uri Href { get; set; }

        [DataMember]
        public Dictionary<string, string> Metadata { get; set; }

        [DataMember]
        public Dictionary<string, List<Dictionary<string, string>>> Legends { get; set; }

        [DataMember]
        public List<string> Samples { get; set; }
    }


    [DataContract(Name = "Variant")]
    public class Variant : AbstractResource
    {
        [DataMember(IsRequired = true)]
        public override string Id { get; set; }

        [DataMember]
        public override Uri Href { get; set; }

        [DataMember]
        public string CHROM { get; set; }

        [DataMember]
        public long POS { get; set; }

        [DataMember]
        public string[] ID { get; set; }

        [DataMember]
        public string REF { get; set; }

        [DataMember]
        public string ALT { get; set; }

        [DataMember]
        public double QUAL { get; set; } 

        [DataMember]
        public string FILTER { get; set; }

        [DataMember]
        public Dictionary<string, string[]> INFO { get; set; }

        [DataMember]
        public Dictionary<string, Dictionary<string, string>> SampleFormat { get; set; }
    }

    public enum VariantSortByParameters { Position }
}
