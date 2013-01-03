using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public abstract class AbstractResourceListRequest<TSortFieldEnumType>  where TSortFieldEnumType : struct
    {
        public string Id { get; set; }

        public int? Offset { get; set; }

        public int? Limit { get; set; }

        public SortDirection? SortDir { get; set; }

        public TSortFieldEnumType? SortBy { get; set; }
    }
}
