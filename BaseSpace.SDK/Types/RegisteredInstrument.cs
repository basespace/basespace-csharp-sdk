using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract]
    public class RegisteredInstrumentCompact
    {
        [DataMember]
        public long InstrumentId { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
    [DataContract]
    public class RegisteredInstrument : RegisteredInstrumentCompact
    {
        [DataMember]
        public string SerialNumber { get; set; }
        
        [DataMember]
        public bool HasValidApiKey { get; set; }
    }
}
