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

        /// <summary>
        /// List of properties that will be set as part of the request
        /// </summary>
        [DataMember]
        public List<PropertyToSet> Properties { get; set; }

        /// <summary>
        /// Include a new property with a given name and description
        /// </summary>
        public PropertyToSet AddProperty(string propertyName, string description = "")
        {
            var p = new PropertyToSet(propertyName, description);
            Properties.Add(p);
            return p;
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

        /// <summary>
        /// Sets the property content to a single-value literal (such as a number, string, etc)
        /// </summary>
        /// <remarks>
        /// Content may not be bigger than 65000 bytes.
        /// </remarks>
        public void SetSingleValueContent(string stringContent)
        {
            Type = PropertyTypes.STRING;
            Content = stringContent;
            Items = null;
        }

        /// <summary>
        /// Sets the property content to a single-value resource reference
        /// </summary>
        public void SetSingleValueReference(IPropertyContent referencedContent)
        {
            Type = referencedContent.Type;
            Content = referencedContent.Href.ToString();
            Items = null;
        }

        /// <summary>
        /// Sets the property content to a single-value resource reference given its relative API URI (ex: 'appresults/123' or 'projects/123' or 'samples/123')
        /// </summary>
        public void SetSingleValueReference(string href)
        {
            Content = href;
            Items = null;
        }

        /// <summary>
        /// Sets the property content to an array of literals (such as a number, string, etc)
        /// </summary>
        /// <remarks>
        /// Each item may not be bigger than 65000 bytes.
        /// </remarks>
        public void SetMultiValueContent(string[] stringContentItems)
        {
            if (stringContentItems != null && stringContentItems.Any())
            {
                Type = PropertyTypes.STRING + PropertyTypes.LIST_SUFFIX;
                Items = stringContentItems;
                Content = null;
            }
        }

        /// <summary>
        /// Sets the property content to an array of resource references (such as a list of Samples, Projects, etc). 
        /// </summary>
        /// <remarks>
        /// Note that all items must be of the same type. 
        /// The first item is used to determine the property type.
        /// </remarks>
        public void SetMultiValueReferences(IPropertyContent[] referencedResourcesContent)
        {
            if (referencedResourcesContent != null && referencedResourcesContent.Any())
            {
                Type = referencedResourcesContent.First().Type + PropertyTypes.LIST_SUFFIX;
                Items = referencedResourcesContent.Where(rr => rr.Type == Type).Select(rr => rr.Href.ToString()).ToArray();
                Content = null;
            }
        }

        /// <summary>
        /// Sets the property content to an array of resource references (such as a list of Samples, Projects, etc) given API URIs to the resources. 
        /// </summary>
        /// <remarks>
        /// Note that all items must be of the same type. 
        /// The first item is used to determine the property type.
        /// </remarks>
        /// <example>
        /// {"appresults/1", "appresults/2", "appresults/10"}
        /// </example>
        public void SetMultiValueReferences(string[] hrefs)
        {
            Items = hrefs;
        }
    }
}