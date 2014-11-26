using System;
using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract( Name = "AppSession")]
    public class AppSessionCompact : AbstractResource, IPropertyContent
    {
        [DataMember(IsRequired = true)]
        public override string Id { get; set; }

        [DataMember(IsRequired = true)]
        public override Uri Href { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public ApplicationCompact Application { get; set; }

        [DataMember]
        public UserCompact UserCreatedBy { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string StatusSummary { get; set; }

        [DataMember]
        public DateTime DateCreated { get; set; }

        public override string ToString()
        {
            return string.Format("Href: {0}; Name: {1}; Status: {2}", Href, Name, Status);
        }

        public string Type
        {
            get { return PropertyTypes.APPSESSION; }
        }
    }


    [DataContract]
    public class AppSession : AppSessionCompact, IPropertyContainingResource
    {
        [DataMember]
        public string OriginatingUri { get; set; }

        [DataMember]
        public IContentReference<IAbstractResource>[] References { get; set; }

        [DataMember]
        public PropertyContainer Properties { get; set; }

        [DataMember]
        public Uri HrefLogs { get; set; }

        [DataMember]
        public AppSessionCompact AppSessionRoot { get; set; }
    }

    public enum AppSessionStatus { Running, Complete, NeedsAttention, Aborted }
    public enum AppSessionQueryParameters { Status, StatusSummary }
    public enum AppSessionSortByParameters { Id, Name, DateCreated }
}
