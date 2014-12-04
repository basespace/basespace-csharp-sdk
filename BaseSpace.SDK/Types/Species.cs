using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract]
    public class Species
    {
        [DataMember(IsRequired = true)]
        public string Id { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}

