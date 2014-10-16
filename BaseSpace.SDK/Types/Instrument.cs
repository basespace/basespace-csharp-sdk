using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract(Name = "InstrumentCompact")]
    public class InstrumentCompact
    {
        [DataMember]
        public long InstrumentId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string SerialNumber { get; set; }
    }
}
