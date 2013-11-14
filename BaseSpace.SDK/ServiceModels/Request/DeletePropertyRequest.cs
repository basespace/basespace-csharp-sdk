using System;
using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    [DataContract]
    public class DeletePropertyRequest : AbstractRequest<DeletePropertyResponse>
    {
        public DeletePropertyRequest()
        {
            HttpMethod = HttpMethods.DELETE;
        }

        public DeletePropertyRequest(IPropertyContainingResource parentResource, string name)
            : this()
        {
            HrefParentResource = parentResource.Href;
            Name = name;
        }

        public Uri HrefParentResource { get; set; }

        public string Name { get; set; }

        protected override string GetUrl()
        {
            if (HrefParentResource == null)
            {
                return null;
            }
            return string.Format("{0}/{1}/{2}", HrefParentResource.ToString(), "properties", Name ?? string.Empty);
        }  
    }
}