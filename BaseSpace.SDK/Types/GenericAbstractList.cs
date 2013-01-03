using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract]
    public class GenericResourceList<TResourceType, TSortFieldType> where TSortFieldType : struct
    {
        [DataMember(IsRequired = true, Order = 0, EmitDefaultValue = false)]
        public TResourceType[] Items { get; set; }

        [DataMember(IsRequired = false, Order = 1, EmitDefaultValue = false)]
        public int? DisplayedCount { get; set; }

        [DataMember(IsRequired = false, Order = 2, EmitDefaultValue = false)]
        public int? TotalCount { get; set; }

        [DataMember(IsRequired = false, Order = 3, EmitDefaultValue = false)]
        public int Offset { get; set; }

        [DataMember(IsRequired = false, Order = 4, EmitDefaultValue = false)]
        public int Limit { get; set; }

        [DataMember(IsRequired = false, Order = 5, EmitDefaultValue = false)]
        public SortDirection? SortDir { get; set; }

        [DataMember(IsRequired = false, Order = 6, EmitDefaultValue = false)]
        public TSortFieldType? SortBy { get; set; }
    }
}
