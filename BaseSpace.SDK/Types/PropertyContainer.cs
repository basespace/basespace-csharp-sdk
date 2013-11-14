using System;
using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract]
    public class PropertyContainer
    {
        [DataMember]
        public PropertyCompact[] Items { get; set; }

        [DataMember]
        public int? DisplayedCount { get; set; }

        [DataMember]
        public int? TotalCount { get; set; }

        [DataMember]
        public Uri Href { get; set; }
    }
}
