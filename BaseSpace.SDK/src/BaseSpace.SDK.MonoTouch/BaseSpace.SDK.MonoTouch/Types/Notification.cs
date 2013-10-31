using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.Types
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
