using System;
using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract(Name = "Run")]
    public class RunCompact : AbstractResource
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
        public UserCompact UserOwnedBy { get; set; }

    }

    [DataContract]
    public class Run : RunCompact
    {
        [DataMember]
        public Uri HrefFiles{ get; set; }

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
