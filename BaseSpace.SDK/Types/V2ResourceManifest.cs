using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Illumina.BaseSpace.SDK.Types
{
    public enum V2ResourceManifestSortFields { /*No sort supported for Resource Manifest Api*/ }

    [DataContract(Name = "ResourceManifest")]
    public class V2ResourceManifestCompact
    {
        [DataMember]
        public ProjectCompact Project { get; set; }
        [DataMember]
        public V2AppSessionCompact AppSession { get; set; }
        [DataMember]
        public V2DatasetCompact Dataset { get; set; }
        [DataMember]
        public V1pre3RunCompact Run { get; set; }
        [DataMember]
        public Uri HrefFiles { get; set; }
        [DataMember]
        public Uri HrefResourceManifest { get; set; }
        [DataMember]
        public long SizeInBytes { get; set; }
    }

    public class V2ResourceManifest : V2ResourceManifestCompact
    {
        public V2ResourceManifestCompact[] Items { get; set; }
        public V2Paging<V2ResourceManifestSortFields> Paging { get; set; }
    }
}
