using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract]
    public class PropertyContainer
    {
        [DataMember]
        public Property[] Items { get; set; }

        [DataMember]
        public int? DisplayedCount { get; set; }

        [DataMember]
        public int? TotalCount { get; set; }

        [DataMember]
        public Uri Href { get; set; }
    }
}
