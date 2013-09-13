using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class SearchRequest : AbstractResourceListRequest<SearchResponse, SearchResultSortByParameters>
    {
        public SearchRequest(string query, string scope)
        {
            Query = query;
            Scope = scope;
        }

        public string Query { get; set; }

        public string Scope { get; set; }

        protected override string GetUrl()
        {
            return string.Format("{0}/search", Version);
        }
    }
}
