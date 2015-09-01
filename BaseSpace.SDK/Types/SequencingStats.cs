using System;
using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract(Name = "SequencingStats")]
    public class SequencingStatsCompact
    {
        [DataMember(IsRequired = true)]
        public Uri Href { get; set; }

        [DataMember]
        public bool IsIndexed { get; set; }

        [DataMember]
        public float YieldTotal { get; set; }

        [DataMember]
        public float ProjectedTotalYield { get; set; }

        [DataMember]
        public float PercentAligned { get; set; }

        [DataMember]
        public float ErrorRate { get; set; }

        [DataMember]
        public float IntensityCycle1 { get; set; }

        [DataMember]
        public float PercentGtQ30 { get; set; }

        [DataMember]
        public float NonIndexedYieldTotal { get; set; }

        [DataMember]
        public float NonIndexedProjectedTotalYield { get; set; }

        [DataMember]
        public float NonIndexedPercentAligned { get; set; }

        [DataMember]
        public float NonIndexedErrorRate { get; set; }

        [DataMember]
        public float NonIndexedIntensityCycle1 { get; set; }

        [DataMember]
        public float NonIndexedPercentGtQ30 { get; set; }

        [DataMember]
        public int MaxCycleCalled { get; set; }

        [DataMember]
        public int MaxCycleExtracted { get; set; }

        [DataMember]
        public int MaxCycleScored { get; set; }

        [DataMember]
        public string Chemistry { get; set; }

        [DataMember]
        public int NumLanes { get; set; }

        [DataMember]
        public int NumSurfaces { get; set; }

        [DataMember]
        public int NumSwathsPerLane { get; set; }

        [DataMember]
        public int NumTilesPerSwath { get; set; }

        [DataMember]
        public int NumReads { get; set; }

        [DataMember]
        public int NumCyclesRead1 { get; set; }

        [DataMember]
        public int NumCyclesRead2 { get; set; }

        [DataMember]
        public int NumCyclesIndex1 { get; set; }

        [DataMember]
        public int NumCyclesIndex2 { get; set; }

        [DataMember]
        public float PercentResynthesis { get; set; }
    }
}
