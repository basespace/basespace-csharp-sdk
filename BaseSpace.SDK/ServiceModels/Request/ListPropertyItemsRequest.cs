using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    [DataContract]
    public class ListPropertyItemsRequest : AbstractResourceListRequest<ListPropertyItemsResponse, PropertyItemsSortByParameters>
    {
        public ListPropertyItemsRequest()
        {
            HttpMethod = HttpMethods.GET;
        }

        public ListPropertyItemsRequest(IPropertyContainingResource parentResource, string propertyName)
            : this()
        {
            HrefParentResource = parentResource.Href;
            PropertyName = propertyName;
        }

        public Uri HrefParentResource { get; set; }

        public string PropertyName { get; set; }

        internal override string GetLogMessage()
        {
            return string.Empty;
        }

        protected override string GetUrl()
        {
            if (HrefParentResource == null || string.IsNullOrEmpty(PropertyName))
            {
                return null;
            }
            return string.Format("{0}/{1}/{2}/{3}", HrefParentResource.ToString(), "properties", PropertyName, "items");
        }  
    }
}