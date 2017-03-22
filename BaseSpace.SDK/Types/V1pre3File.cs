using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;

namespace Illumina.BaseSpace.SDK.Types
{
    public enum V1pre3FilesSortFields { Id, DateCreated, Path, Name }

    [DataContract(Name = "File")]
    public class V1pre3FileCompact : AbstractResource
    {
        [DataMember(IsRequired = true)]
        public override string Id { get; set; }

        [DataMember(IsRequired = true)]
        public override Uri Href { get; set; }

        [DataMember]
        public string HrefContent { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string ContentType { get; set; }

        [DataMember]
        public long Size { get; set; }

        [DataMember]
        public string Path { get; set; }

        [DataMember]
        public DateTime DateCreated { get; set; }

        [DataMember]
        public SampleCompact ParentSample { get; set; }

        [DataMember]
        public AppResultCompact ParentAppResult { get; set; }

        [DataMember]
        public V1pre3RunCompact ParentRun { get; set; }

        [DataMember]
        public GenomeCompact ParentGenome { get; set; }

        [DataMember]
        public AppSessionCompact ParentAppSession { get; set; }

        [DataMember]
        public List<ProjectCompact> ParentProjects { get; set; }

        [DataMember]
        public string Category { get; set; }

        //File e-tag as defined by AWS
        [DataMember]
        public string ETag { get; set; }

        // V2+ ONLY
        [DataMember]
        public V2DatasetCompact ParentDataset { get; set; }
    }
}
