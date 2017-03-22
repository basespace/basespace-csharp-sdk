using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Illumina.BaseSpace.SDK.Types
{
    public class V2Paging<TSortField> where TSortField : struct
    {
        public int? DisplayedCount { get; set; }
        public int? TotalCount { get; set; }
        public virtual int? Offset { get; set; }
        public virtual int? Limit { get; set; }
        // Note: enum property doesn't need to be a string (for proper error handling) as this is an outgoing parameter
        public SortDirection? SortDir { get; set; }
        // Note: enum property doesn't need to be a string (for proper error handling) as this is an outgoing parameter
        public TSortField? SortBy { get; set; }
    }
}
