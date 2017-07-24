using System;
using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK
{
    public enum FileContentRedirectType { True, Proxy, Meta }

    [DataContract]
    public class FileContentRedirectMeta
    {
        [DataMember(IsRequired = true)]
        public DateTime Expires { get; set; }

        [DataMember(IsRequired = true)]
        public string HrefContent { get; set; }

        [DataMember(IsRequired = true)]
        public bool SupportsRange { get; set; }
    }
}