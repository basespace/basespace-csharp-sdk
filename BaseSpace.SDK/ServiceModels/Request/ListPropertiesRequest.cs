using System;
using System.Runtime.Serialization;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    [DataContract]
    public class ListPropertiesRequest : AbstractNestedResourceListRequest<ListPropertiesResponse, PropertiesSortByParameters>
    {
        public ListPropertiesRequest()
        {
            HttpMethod = HttpMethods.GET;
        }

        public ListPropertiesRequest(IPropertyContainingResource parentResource)
            : this()
        {
            HrefParentResource = parentResource.Href;
        }

        public Uri HrefParentResource { get; set; }

        protected override string GetUrl()
        {
            if (HrefParentResource == null)
            {
                return null;
            }
            return string.Format("{0}/{1}", HrefParentResource.ToString(), "properties");
        }  
    }
}