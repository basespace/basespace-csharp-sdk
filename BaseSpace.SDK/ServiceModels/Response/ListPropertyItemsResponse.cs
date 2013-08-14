using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
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
