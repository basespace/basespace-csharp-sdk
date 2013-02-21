using System;
using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract]
    public class ScheduledDowntime
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public DateTime StartedOn { get; set; }

        [DataMember]
        public DateTime? ExpectedToEndOn { get; set; }

    }
}

