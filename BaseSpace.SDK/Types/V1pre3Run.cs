using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;

namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract(Name = "Run")]
    public class V1pre3RunCompact : AbstractResource
    {
        [DataMember(IsRequired = true)]
        public override string Id { get; set; }

        [DataMember(IsRequired = true)]
        public override Uri Href { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Number { get; set; }

        [DataMember]
        public string ExperimentName { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string ReagentBarcode { get; set; }

        [DataMember]
        public string FlowcellBarcode { get; set; }

        [DataMember]
        public string BufferBarcode { get; set; }

        [DataMember]
        public DateTime DateCreated { get; set; }

        [DataMember]
        public DateTime DateModified { get; set; }

        [DataMember]
        public string AggregateRunState { get; set; }

        [DataMember]
        public UserCompact UserOwnedBy { get; set; }

        [DataMember]
        [Description("If the run is locked, designates the user holding the lock")]
        public UserCompact UserLockedBy { get; set; }

        [DataMember]
        [Description("If the run is locked, designates the instrument (if any) holding the lock")]
        public RegisteredInstrumentCompact InstrumentLockedBy { get; set; }

        [DataMember]
        public long TotalSize { get; set; }

        [DataMember]
        public string PlatformName { get; set; }

        [DataMember]
        public string Workflow { get; set; }

        [DataMember]
        public RegisteredInstrumentCompact Instrument { get; set; }

        [DataMember]
        public string InstrumentName { get; set; }

        [DataMember]
        public string InstrumentType { get; set; }

        [DataMember]
        public int NumCyclesRead1 { get; set; }

        [DataMember]
        public int NumCyclesRead2 { get; set; }

        [DataMember]
        public int NumCyclesIndex1 { get; set; }

        [DataMember]
        public int NumCyclesIndex2 { get; set; }

        [DataMember]
        public Uri HrefBaseSpaceUI { get; set; }

        [DataMember] // NOTE (maxm): This isn't populated by search currently
        public bool HasCollaborators { get; set; }

        [DataMember] // NOTE (maxm): This isn't populated by search currently
        public bool? IsTransferPending { get; set; }

        /// <summary>
        /// Refers to metadata only, not the files themselves
        /// True if resource is in the trash or purged. Such resources cannot generally be accessed directly (Ie, GET: resource/{id})
        /// but may be referenced by something else such as within a property. This gives an indication that the referenced data is not accessible
        /// </summary>
        [DataMember]
        // IsMetadataDeleted
        public bool? IsDeleted { get; set; }

        [DataMember]
        public bool? IsFileDataDeleted { get; set; }

        [DataMember]
        public SequencingStatsCompact SequencingStats { get; set; }

        [DataMember]
        public PrepSettings PrepSettings { get; set; }

        [DataMember]
        public DateTime? ExpectedInstrumentCompletionDate { get; set; }

        [DataMember]
        public string PrepErrorCode { get; set; }

        [DataMember]
        public AnalysisSettings AnalysisSettings { get; set; }

        [DataMember]
        public string LaneAndQcStatus { get; set; }

        [DataMember]
        public string LimsStatus { get; set; }
    }

    [DataContract(Name = "AnalysisSettings")]
    public class AnalysisSettings
    {
        [DataMember]
        public bool ReverseComplementI5Index { get; set; }
    }

    public class RegisteredInstrumentCompact
    {
        [DataMember]
        public long InstrumentId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string SerialNumber { get; set; }
    }
}
