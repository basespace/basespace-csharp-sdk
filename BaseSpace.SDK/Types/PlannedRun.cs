using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;

namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract(Name="PlannedRun")]
    public class PlannedRun : RunCompact
    {
		[DataMember]
		[Description("If the run is locked, designates the user holding the lock")]
		public UserCompact UserLockedBy { get; set; }

        [DataMember]
        [Description("If the run is locked, designates the instrument (if any) holding the lock")]
        public RegisteredInstrumentCompact InstrumentLockedBy { get; set; }

        [DataMember]
        public string ReagentBarcode { get; set; }

        [DataMember]
        public string FlowcellBarcode { get; set; }

        [DataMember]
        public string AnalysisWorkflowType { get; set; }

        [DataMember]
        public string IndexingStrategy { get; set; }

        [DataMember]
        public bool CustomPrimerR1 { get; set; }

        [DataMember]
        public bool CustomPrimerR2 { get; set; }

        [DataMember]
        public bool CustomPrimerIndex { get; set; }
        
        [DataMember]
        public string FlowcellFormat { get; set; }

        [DataMember]
        public string InstrumentRunMode { get; set; }

        [DataMember]
        public bool OnBoardClusterGeneration { get; set; }

        [DataMember]
        public bool ConfirmFirstBase { get; set; }

        [DataMember]
        public int ControlLane { get; set; }

        [DataMember]
        public bool OverrideIndexCycles { get; set; }

        [DataMember]
        public bool Rehyb { get; set; }

        [DataMember]
        public bool HasLibraryWarnings { get; set; }
    }

    [DataContract]
    public class RunLibraryPoolMapping 
    {
        [DataMember]
        public LibraryPoolCompact LibraryPool { get; set; }

        [DataMember]
        public string Lane { get; set; }
    }

    [DataContract]
    public class RunLibraryMapping
    {
        [DataMember]
        public SampleLibrary Library { get; set; }

        [DataMember]
        public string Lane { get; set; }
    }

    [DataContract]
    public class PlannedRunPoolMapping
    {
        [DataMember(IsRequired = true)]
        public long PoolId { get; set; }

        [DataMember]
        public string Lane { get; set; }
    }
}

