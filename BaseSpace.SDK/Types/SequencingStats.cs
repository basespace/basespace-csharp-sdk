using System;
using System.Collections.Generic;
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

    [DataContract(Name = "SequencingStats")]
    public class SequencingStats : SequencingStatsCompact
    {

        [DataMember]
        public IEnumerable<Read> Reads { get; set; }

        [DataMember]
        public IEnumerable<Lane> Lanes { get; set; }

        [DataMember]
        public IEnumerable<LaneByRead> LanesByRead { get; set; }
    }

    [DataContract(Name = "Read")]
    public class Read
    {
        [DataMember]
        public int ReadNumber { get; set; }

        [DataMember]
        public bool IsIndexed { get; set; }

        [DataMember]
        public int TotalCycles { get; set; }

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
    }

    [DataContract(Name = "Lane")]
    public class Lane
    {
        [DataMember]
        public int LaneNumber { get; set; }

        [DataMember]
        public float Density { get; set; }

        [DataMember]
        public float PercentPf { get; set; }

        [DataMember]
        public float Phasing { get; set; }

        [DataMember]
        public float PrePhasing { get; set; }

        [DataMember]
        public long Reads { get; set; }

        [DataMember]
        public long ReadsPf { get; set; }

        [DataMember]
        public float PercentGtQ30 { get; set; }

        [DataMember]
        public float Yield { get; set; }

        [DataMember]
        public int MinCycleCalled { get; set; }

        [DataMember]
        public int MaxCycleCalled { get; set; }

        [DataMember]
        public float PercentAligned { get; set; }

        [DataMember]
        public float ErrorRate { get; set; }

        [DataMember]
        public float ErrorRate35 { get; set; }

        [DataMember]
        public float ErrorRate50 { get; set; }

        [DataMember]
        public float ErrorRate75 { get; set; }

        [DataMember]
        public float ErrorRate100 { get; set; }

        [DataMember]
        public float IntensityCycle1 { get; set; }
    }

    [DataContract(Name = "LaneByRead")]
    public class LaneByRead
    {
        [DataMember]
        public int ReadNumber { get; set; }

        [DataMember]
        public int LaneNumber { get; set; }

        [DataMember]
        public int TileCount { get; set; }

        [DataMember]
        public float Density { get; set; }

        [DataMember]
        public float DensityDeviation { get; set; }

        [DataMember]
        public float PercentPf { get; set; }

        [DataMember]
        public float PercentPfDeviation { get; set; }

        [DataMember]
        public float Phasing { get; set; }

        [DataMember]
        public float PrePhasing { get; set; }

        [DataMember]
        public long Reads { get; set; }

        [DataMember]
        public long ReadsPf { get; set; }

        [DataMember]
        public float PercentGtQ30 { get; set; }

        [DataMember]
        public float Yield { get; set; }

        [DataMember]
        public int MinCycleCalled { get; set; }

        [DataMember]
        public int MaxCycleCalled { get; set; }

        [DataMember]
        public int MinCycleError { get; set; }

        [DataMember]
        public int MaxCycleError { get; set; }

        [DataMember]
        public float PercentAligned { get; set; }

        [DataMember]
        public float PercentAlignedDeviation { get; set; }

        [DataMember]
        public float ErrorRate { get; set; }

        [DataMember]
        public float ErrorRateDeviation { get; set; }

        [DataMember]
        public float ErrorRate35 { get; set; }

        [DataMember]
        public float ErrorRate35Deviation { get; set; }

        [DataMember]
        public float ErrorRate50 { get; set; }

        [DataMember]
        public float ErrorRate50Deviation { get; set; }

        [DataMember]
        public float ErrorRate75 { get; set; }

        [DataMember]
        public float ErrorRate75Deviation { get; set; }

        [DataMember]
        public float ErrorRate100 { get; set; }

        [DataMember]
        public float ErrorRate100Deviation { get; set; }

        [DataMember]
        public float IntensityCycle1 { get; set; }

        [DataMember]
        public float IntensityCycle1Deviation { get; set; }
    }
}
