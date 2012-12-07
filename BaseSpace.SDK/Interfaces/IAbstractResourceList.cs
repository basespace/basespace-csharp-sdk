using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumina.BaseSpace.SDK.Models;

namespace Illumina.BaseSpace.SDK
{
    public interface IAbstractResourceList
    {
        int? Offset { get; set; }
        int? Limit { get; set; }
        SortDirection SortDir { get; set; }
    }
}
