using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Illumina.BaseSpace.SDK.Models
{
    public enum SortDirection { Asc, Desc }
    
    public class AbstractResourceList : IAbstractResourceList
    {
        [DataMember]
        public int? Offset { get; set; }

        [DataMember]
        public int? Limit { get; set; }

        [DataMember]
        public SortDirection SortDir { get; set; }
    }
}
