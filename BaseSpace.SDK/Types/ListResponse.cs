using System.Linq;
using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract]
    public class ResourceList<TResourceType, TSortFieldType> where TSortFieldType : struct
    {
        [DataMember]
        public TResourceType[] Items { get; set; }

        [DataMember]
        public int DisplayedCount { get; set; }

        [DataMember]
        public int TotalCount { get; set; }

        [DataMember]
        public int Offset { get; set; }

        [DataMember]
        public int Limit { get; set; }

        [DataMember]
        public SortDirection? SortDir { get; set; }

        [DataMember]
        public TSortFieldType? SortBy { get; set; }
    }

    [DataContract]
    public class ListResponse<TCompactResourceType, TSortFieldEnumType> : ApiResponse<ResourceList<TCompactResourceType, TSortFieldEnumType>> where TSortFieldEnumType : struct
    {
        private ResourceList<TCompactResourceType, TSortFieldEnumType> _resourceList;

        public ListResponse(TCompactResourceType[] items, int totalCount, int offset, int limit, TSortFieldEnumType? sortBy, SortDirection? sortDir)
        {
            _resourceList = new ResourceList<TCompactResourceType, TSortFieldEnumType>
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
