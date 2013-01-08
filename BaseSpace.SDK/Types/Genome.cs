﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract(Name = "Genome")]
    public class GenomeCompact : AbstractResource
    {
        [DataMember(IsRequired = true)]
        public override string Id { get; set; }

        [DataMember]
        public string SpeciesName { get; set; }

        [DataMember]
        public override Uri Href { get; set; }
    }

    [DataContract()]
    public class Genome : GenomeCompact
    {
        [DataMember]
        public string DisplayName { get; set; }

        [DataMember]
        public string Source { get; set; }

        [DataMember]
        public string Build { get; set; }
    }

    public enum GenomeSortByParameters { Id, SpeciesName, Build }
}
