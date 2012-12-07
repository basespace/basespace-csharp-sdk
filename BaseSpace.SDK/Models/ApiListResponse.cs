using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Illumina.BaseSpace.SDK.Models
{
    [DataContract]
    public class ResourceList<TResourceType, TSortFieldType> where TSortFieldType : struct
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

    [DataContract]
    public class ApiListResponse<TCompactResourceType, TSortFieldEnumType> : ApiResponse<ResourceList<TCompactResourceType, TSortFieldEnumType>> where TSortFieldEnumType : struct
    {
        private ResourceList<TCompactResourceType, TSortFieldEnumType> _resourceList;

        public ApiListResponse(TCompactResourceType[] items, int? totalCount, int offset, int limit, TSortFieldEnumType? sortBy, SortDirection? sortDir)
        {
            _resourceList = new ResourceList<TCompactResourceType, TSortFieldEnumType>()
            {
                Items = items,
                DisplayedCount = items.Count(),
                TotalCount = totalCount,
                Offset = offset,
                Limit = limit,
                SortBy = sortBy,
                SortDir = sortDir
            };
        }

        [DataMember(IsRequired = true)]
        public override ResourceList<TCompactResourceType, TSortFieldEnumType> Response { get { return _resourceList; } set { _resourceList = value; } }
    }


}
