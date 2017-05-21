using System;
using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.Types
{

    [DataContract( Name = "Application")]
    public class ApplicationCompact : AbstractResource
    {
        [DataMember(IsRequired = true)]
        public override string Id { get; set; }

        [DataMember(IsRequired = true)]
        public override Uri Href { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string CompanyName { get; set; }

        [DataMember]
        public string Category { get; set; }

        [DataMember]
        public string[] Classifications { get; set; }

        [DataMember]
        public string HrefLogo { get; set; }

        [DataMember]
        public string HomepageUri { get; set; }

        [DataMember]
        public string ShortDescription { get; set; }

        [DataMember]
        public string DateCreated { get; set; }

        [DataMember]
        public string PublishStatus { get; set; }

        [DataMember]
        public bool IsBillingActivated { get; set; }

        public override string ToString()
        {
            return string.Format("Href: {0}; Name: {1}", Href, Name);
        }
    }
    [DataContract]
    public class Application : ApplicationCompact, IPropertyContainingResource
    {
        [DataMember]
        public string LongDescription { get; set; }

        [DataMember]
        public UserCompact UserOwnedBy { get; set; }

        [DataMember]
        public Uri HrefSettings { get; set; }

        [DataMember]
        public PropertyContainer Properties { get; set; }
    }

    public enum ApplicationSortByParameters { Id, Name, DateCreated }
}
