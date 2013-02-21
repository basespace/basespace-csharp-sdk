using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract]
    public class Notification<T> : INotification<T>
    {
        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public T Item { get; set; }
        
    }
}
