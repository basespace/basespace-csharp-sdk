using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class ListRunsRequest : AbstractResourceListRequest<RunSortByParameters>
    {
        public string Status { get; set; }
    }
}
