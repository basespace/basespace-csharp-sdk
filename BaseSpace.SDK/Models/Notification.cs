using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Illumina.BaseSpace.SDK.Models
{
    [DataContract]
    [KnownType(typeof(Agreement))]
    public class Notification : INotification<object>
    {
        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public object Item { get; set; }
    }
}
