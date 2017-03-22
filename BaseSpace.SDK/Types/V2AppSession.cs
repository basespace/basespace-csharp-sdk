using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;

namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract(Name = "AppSession")]
    public class V2AppSessionCompact : AbstractResource
    {
        [DataMember(IsRequired = true)]
        public override string Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember(IsRequired = true)]
        public override Uri Href { get; set; }

        [DataMember]
        public ApplicationCompact Application { get; set; }

        [DataMember]
        public UserCompact UserCreatedBy { get; set; }

        [DataMember]
        public string ExecutionStatus { get; set; }

        [DataMember]
        public string QcStatus { get; set; }

        [DataMember]
        public string StatusSummary { get; set; }

        [DataMember]
        public DateTime DateCreated { get; set; }

        [DataMember]
        public DateTime DateModified { get; set; }

        [DataMember]
        public DateTime? DateCompleted { get; set; }

        [DataMember]
        public long? TotalSize { get; set; }

        [DataMember]
        public PropertyContainer Properties { get; set; }

        /// <summary>
        /// True if resource is in the trash or purged. Such resources cannot generally be accessed directly (Ie, GET: resource/{id})
        /// but may be referenced by something else such as within a property. This gives an indication that the referenced data is not accessible
        /// </summary>
        [DataMember]
        public bool? IsDeleted { get; set; }

        [DataMember]
        public string DeliveryStatus { get; set; }

        [DataMember]
        public bool ContainsComments { get; set; }

        /// <summary>
        /// Optional reference to the AppSession root
        /// </summary>
        [DataMember]
        public V2AppSessionCompact AppSessionRoot { get; set; }

        [DataMember]
        public Uri HrefComments { get; set; }
    }

    [DataContract(Name = "AppSession")]
    public class V2AppSession : V2AppSessionCompact
    {
        [DataMember]
        public string OriginatingUri { get; set; }

        [DataMember]
        public string HrefLogs { get; set; }

        [DataMember]
        public V2AppSessionCompact[] Children { get; set; }

        [DataMember]
        public int? RunningDuration { get; set; }

        [DataMember]
        public ComputeStatistics ComputeStatistics { get; set; }
    }

    [DataContract(Name = "ComputeStatistics")]
    public class ComputeStatistics
    {
        [DataMember]
        public string Unit { get; set; }

        [DataMember]
        public decimal Amount { get; set; }
    }
}
