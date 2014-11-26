using System.Runtime.Serialization;

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
