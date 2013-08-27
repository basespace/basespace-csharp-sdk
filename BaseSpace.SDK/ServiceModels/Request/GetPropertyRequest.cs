using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    [DataContract]
    public class GetPropertyRequest : AbstractRequest<GetPropertyResponse>
    {
        public GetPropertyRequest()
        {
            HttpMethod = HttpMethods.GET;
        }


        public GetPropertyRequest(IPropertyContainingResource parentResource, string propertyName)
            : this()
        {
            HrefParentResource = parentResource.Href;
            PropertyName = propertyName;
        }


        public Uri HrefParentResource { get; set; }

        public string PropertyName { get; set; }

        protected override string GetUrl()
        {
            if (HrefParentResource == null || string.IsNullOrEmpty(PropertyName))
            {
                return null;
            }
            return string.Format("{0}/{1}/{2}", HrefParentResource.ToString(), "properties", PropertyName);
        }
    }
}
