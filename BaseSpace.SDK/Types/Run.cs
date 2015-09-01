using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract(Name = "Run")]
    public class RunCompact : AbstractResource, IPropertyContent
    {
        [DataMember(IsRequired = true)]
        public override string Id { get; set; }

        [DataMember(IsRequired = true)]
        public override Uri Href { get; set; }

        [DataMember]
        public string ExperimentName { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public DateTime DateCreated { get; set; }

        [DataMember]
        public DateTime DateModified { get; set; }

        [DataMember]
        public UserCompact UserLockedBy { get; set; }

        [DataMember]
        public InstrumentCompact InstrumentLockedBy { get; set; }

        [DataMember]
        public string ReagentBarcode { get; set; }

        [DataMember]
        public string FlowcellBarcode { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Number { get; set; }

        [DataMember]
        public InstrumentCompact Instrument { get; set; }

        [DataMember]
        public UserCompact UserOwnedBy { get; set; }

        [DataMember]
        public PrepSettings PrepSettings { get; set; }

        [DataMember]
        public DateTime? ExpectedInstrumentCompletionDate { get; set; }

        [DataMember]
        public string PrepErrorCode { get; set; }

        public string Type { get { return PropertyTypes.RUN; } }

        [DataMember]
        public string PlatformName { get; set; }

        [DataMember]
        public int NumCyclesRead1 { get; set; }

        [DataMember]
        public int NumCyclesRead2 { get; set; }

        [DataMember]
        public int NumCyclesIndex1 { get; set; }

        [DataMember]
        public int NumCyclesIndex2 { get; set; }

        public override string ToString()
        {
            return string.Format("Href: {0}; Name: {1}; Status: {2}", Href, Name, Status);
        }
    }

    [DataContract]
    public class Run : RunCompact, IPropertyContainingResource
    {
        [DataMember]
        public Uri HrefFiles { get; set; }

        [DataMember]
        public Uri HrefSamples { get; set; }

        [DataMember]
        public UserCompact UserUploadedBy { get; set; }

        [DataMember]
        public DateTime? DateUploadCompleted { get; set; }

        [DataMember]
        public DateTime? DateUploadStarted { get; set; }

        [DataMember]
        public Uri HrefBaseSpaceUI { get; set; }

        [DataMember]
        public PropertyContainer Properties { get; set; }

        [DataMember]
        public SequencingStatsCompact SequencingStats { get; set; }
    }

    [DataContract]
    public class RunSettings
    {
        [DataMember]
        public int NumCyclesRead1 { get; set; }

        [DataMember]
        public int NumCyclesRead2 { get; set; }

        [DataMember]
        public bool CustomPrimerR1 { get; set; }

        [DataMember]
        public bool CustomPrimerR2 { get; set; }

        [DataMember]
        public bool CustomPrimerIndex { get; set; }

        [DataMember]
        public bool AutoRetireOnCompletion { get; set; }
    }

    [DataContract]
    public class PrepSettings
    {
        [DataMember]
        public string LibraryPrepName { get; set; }

        [DataMember]
        public string ProtocolVersion { get; set; }

        [DataMember]
        public List<PrepModule> Modules { get; set; }
    }

    [DataContract]
    public class PrepModule
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<PrepModuleParameter> Parameters { get; set; }
    }

    [DataContract]
    public class PrepModuleParameter
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Value { get; set; }
    }

    public enum RunSortByParameters
    {
        Id,
        DateCreated,
        DateModified
    }

    public enum RunFilesSortByParameters
    {
        Id,
        Path,
        DateCreated
    }
}
