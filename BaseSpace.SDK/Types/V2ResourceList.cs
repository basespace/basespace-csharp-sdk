using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Illumina.BaseSpace.SDK.Types
{
    public class V2ResourceList<TResource, TSortField> where TSortField : struct
    {
        public TResource[] Items { get; set; }

        public V2Paging<TSortField> Paging { get; set; }
    }
}
