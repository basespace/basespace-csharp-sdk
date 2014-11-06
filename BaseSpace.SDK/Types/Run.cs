using System;
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
        public string PrepStatus { get; set; }

        [DataMember]
        public DateTime? ExpectedInstrumentCompletionDate { get; set; }

        [DataMember]
        public PrepErrorStatusType PrepErrorStatus { get; set; }

        [DataMember]
        public string PlatformName { get; set; }

        public string Type { get { return PropertyTypes.RUN; } }

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
        public bool? Quantitate { get; set; }

        [DataMember]
        public bool? Normalize { get; set; }

        [DataMember]
        public PcrCycles PcrCycles { get; set; }

        [DataMember]
        public InsertSize DefaultInsertSize { get; set; }

        [DataMember]
        public float? FragmentationTimeInMinutes { get; set; }
    }

    [DataContract]
    public class PcrCycles
    {
        [DataMember]
        public int ItemValue { get; set; }
    }

    [DataContract]
    public class InsertSize
    {
        [DataMember]
        public int ItemValue { get; set; }
    }

    public enum PrepErrorStatusType
    {
        None,
        Errored
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
