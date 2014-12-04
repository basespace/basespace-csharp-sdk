using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.Types
{
    public enum BiologicalSampleSortByParameters { Id, UserSampleId, Name, DateModified, UserOwnedBy, Species, Project }
    public enum LibraryPrepKitSortFields { Id, Name }

    public enum LibrariesSortFields
    {
        Id = 0,
        UserSampleId = 1,
        Name = 2,
        DateModified = 3,
        Index1Display = 4,
        Index2Display = 5,
        ParentContainerId = 6,
        ParentUserContainerId = 7,
    }

    [DataContract(Name = "BiologicalSampleCompact")]
    public class BiologicalSampleCompact : AbstractResource
    {
        [DataMember(IsRequired = true)]
        public override string Id { get; set; }

        [DataMember]
        public override Uri Href { get; set; }

        [DataMember]
        public string UserSampleId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string NucleicAcid { get; set; }

        [DataMember]
        public Species Species { get; set; }

        [DataMember]
        public ProjectCompact Project { get; set; }

        [DataMember]
        public UserCompact UserOwnedBy { get; set; }

        [DataMember]
        public int LibraryCount { get; set; }

        [DataMember]
        public DateTime DateModified { get; set; }
    }

    [DataContract(Name = "BiologicalSample")]
    public class BiologicalSample : BiologicalSampleCompact
    {
        // add properties later
        [DataMember]
        public Species Species { get; set; }
    }

    [DataContract(Name = "SampleLibrary")]
    public class SampleLibrary : AbstractResource
    {
        [DataMember(IsRequired = true)]
        public override string Id { get; set; }

        [DataMember]
        public override Uri Href { get; set; }

        [DataMember]
        public BiologicalSampleCompact BiologicalSample { get; set; }

        [DataMember]
        public string ContainerPosition { get; set; }

        [DataMember]
        public LibraryIndexCompact Index1 { get; set; }

        [DataMember]
        public LibraryIndexCompact Index2 { get; set; }

        [DataMember]
        public DateTime DateModified { get; set; }

        [DataMember]
        public LibraryPrepKitCompact LibraryPrep { get; set; }

        [DataMember]
        public ProjectCompact Project { get; set; }

        [DataMember]
        public LibraryContainerCompact Container { get; set; }
    }

    public class SampleLibraryRequest
    {
        public string SampleLibraryId { get; set; }
        public string BiologicalSampleId { get; set; }
        public string Position { get; set; }
        public string Index1SequenceId { get; set; }
        public string Index2SequenceId { get; set; }
        public string ProjectId { get; set; }
    }

    [DataContract]
    public class LibraryIndexCompact
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Sequence { get; set; }
    }

    [DataContract(Name = "LibraryContainerCompact")]
    public class LibraryContainerCompact : AbstractResource
    {
        [DataMember(IsRequired = true)]
        public override string Id { get; set; }

        [DataMember]
        public override Uri Href { get; set; }

        [DataMember]
        public string ContainerType { get; set; }

        [DataMember]
        public string UserContainerId { get; set; }

        [DataMember]
        public string LibraryPrep { get; set; }

        [DataMember]
        public LibraryPrepKitCompact LibraryPrepKit { get; set; }

        [DataMember]
        public UserCompact UserOwnedBy { get; set; }

        [DataMember]
        public int LibraryCount { get; set; }

        [DataMember]
        public DateTime DateModified { get; set; }
    }

    [DataContract(Name = "LibraryContainer")]
    public class LibraryContainer : LibraryContainerCompact
    {
        [DataMember]
        public string Notes { get; set; }

        [DataMember]
        public bool IndexByWell { get; set; }
    }

    [DataContract(Name = "LibraryPoolCompact")]
    public class LibraryPoolCompact : AbstractResource
    {
        [DataMember(IsRequired = true)]
        public override string Id { get; set; }

        [DataMember]
        public override Uri Href { get ; set; }

        [DataMember]
        public string UserPoolId { get; set; }

        [DataMember]
        public UserCompact UserOwnedBy { get; set; }

        [DataMember]
        public int LibraryCount { get; set; }

        [DataMember]
        public DateTime DateModified { get; set; }

        [DataMember]
        public ICollection<string> LibraryPrep { get; set; }
    }

    [DataContract(Name = "LibraryPool")]
    public class LibraryPool : LibraryPoolCompact
    {
        [DataMember]
        public string Notes { get; set; }
    }

    [DataContract(Name = "LibraryPrepKitCompact")]
    public class LibraryPrepKitCompact : AbstractResource
    {
        [DataMember(IsRequired = true)]
        public override string Id { get; set; }

        [DataMember]
        public override Uri Href { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public UserCompact UserOwnedBy { get; set; }

        [DataMember]
        public string ValidIndexingStrategies { get; set; }

        [DataMember]
        public string ValidReadTypes { get; set; }

        [DataMember]
        public int NumIndexCycles { get; set; }

        [DataMember]
        public string AdapterSequenceRead1 { get; set; }

        [DataMember]
        public string AdapterSequenceRead2 { get; set; }
    }

    [DataContract(Name = "LibraryPrepKit")]
    public class LibraryPrepKit : LibraryPrepKitCompact
    {
        [DataMember]
        public IEnumerable<LibraryIndexCompact> Index1Sequences { get; set; }

        [DataMember]
        public IEnumerable<LibraryIndexCompact> Index2Sequences { get; set; }
    }
}


