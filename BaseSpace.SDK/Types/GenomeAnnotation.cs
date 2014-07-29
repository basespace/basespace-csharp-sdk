﻿using System;
using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.Types
{
    public enum GenomeAnnotationSortByParameters { Id, Name }
    [DataContract(Name = "Genome")]
    public class GenomeAnnotation : AbstractResource
    {
        [DataMember(IsRequired = true)]
        public override string Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public override Uri Href { get; set; }
        [DataMember]
        public Uri HrefFileSets { get; set; }
    }
}
