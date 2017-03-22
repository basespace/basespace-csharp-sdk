using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;

namespace Illumina.BaseSpace.SDK.Types
{
    public class V2FilesList : V2ResourceList<V1pre3FileCompact, V1pre3FilesSortFields>
    {
        public V1pre3DirectoryCompact[] SubDirectories { get; set; }
    }

    [DataContract]
    public class V1pre3DirectoryCompact
    {
        [DataMember(IsRequired = true)]
        public Uri Href { get; set; }

        [DataMember(IsRequired = true)]
        public string Name { get; set; }
    }

}
