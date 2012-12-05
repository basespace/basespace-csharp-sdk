using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Illumina.BaseSpace.SDK.Models
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
