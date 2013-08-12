using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    [DataContract]
    public class SetPropertiesRequest : AbstractRequest<SetPropertiesResponse>
    {
        public SetPropertiesRequest()
        {
            Properties = new List<PropertyToSet>();
            HttpMethod = HttpMethods.POST;
        }

        public SetPropertiesRequest(IPropertyContainingResource parentResource):this()
        {
            HrefParentResource = parentResource.Href;
        }

        public Uri HrefParentResource { get; set; }

        [DataMember]
        public List<PropertyToSet> Properties { get; set; }

        public PropertyToSet AddProperty(string propertyName, string description = "")
        {
            var p = new PropertyToSet(propertyName, description);
            Properties.Add(p);
            return p;
        }

        internal override string GetLogMessage()
        {
            return string.Empty;
        }

        protected override string GetUrl()
        {
            if (HrefParentResource == null)
            {
                return null;
            }
            return string.Format("{0}/{1}", HrefParentResource.ToString(), "properties");
        }


    }
    [DataContract]
    public class PropertyToSet
    {
        public PropertyToSet(string propertyName, string description = "")
        {
            Name = propertyName;
            Description = description;
        }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Content { get; set; }

        [DataMember]
        public string[] Items { get; set; }

        public void SetSingleValueContent(string stringContent)
        {
            Type = Property.TYPE_STRING;
            Content = stringContent;
        }

        public void SetSingleValueContent(IPropertyContent referencedContent)
        {
            Type = referencedContent.Type;
            Content = referencedContent.Href.ToString();
        }

        public void SetMultiValueContent(string[] stringContentItems)
        {
            if (stringContentItems != null && stringContentItems.Any())
            {
                Type = Property.TYPE_STRING + Property.TYPE_LIST_SUFFIX;
                Items = stringContentItems;
            }
        }

        public void SetMultiValueContent(IPropertyContent[] referencedResourcesContent)
        {
            if (referencedResourcesContent != null && referencedResourcesContent.Any())
            {
                Type = referencedResourcesContent.First().Type + Property.TYPE_LIST_SUFFIX;
                Items = referencedResourcesContent.Where(rr => rr.Type == Type).Select(rr => rr.Href.ToString()).ToArray();
            }
        }
    }
}