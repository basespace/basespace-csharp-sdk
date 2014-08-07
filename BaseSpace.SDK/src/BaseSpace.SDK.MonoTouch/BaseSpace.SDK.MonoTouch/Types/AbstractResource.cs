using System;
using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.Types
{

    [DataContract]
    public abstract  class AbstractResource : IAbstractResource
    {
        [DataMember(IsRequired = true)]
        public abstract string Id { get; set; }

        [DataMember(IsRequired = true)]
        public abstract  Uri Href { get; set; }

    }
}
