using System.Runtime.Serialization;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class ListPropertyItemsResponse : ApiResponse<PropertyItemsResourceList>
    {
    }

    [DataContract]
    public class PropertyItemsResourceList : GenericResourceList<PropertyItem, PropertyItemsSortByParameters>
    {
        [DataMember]
        public string Type { get; set; }
    }
}
