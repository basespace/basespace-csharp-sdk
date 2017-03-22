using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Illumina.BaseSpace.SDK.Types
{
    public class V2DatasetTypeCompact : AbstractResource
    {
        public override string Id { get; set; }
        public override Uri Href { get; set; }
        public string Name { get; set; }
        public virtual string[] ConformsToIds { get; set; }
    }
}
