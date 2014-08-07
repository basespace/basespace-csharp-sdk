using System.Net;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class SearchRequest : AbstractResourceListRequest<SearchResponse, SearchResultSortByParameters>
    {
        public SearchRequest(string query, string scope = null, int? offset = null, int? limit = null, SortDirection? sortDir = null, SearchResultSortByParameters? sortBy = null)
            :base(offset,limit,sortDir,sortBy)
        {
            Query = query;
            Scope = scope;
        }

        /// <summary>
        /// Comma separated list of the types of documents to include in the search results. All documents are included by default.
        /// </summary>
        public string Scope
        {
            get;
            set;
        }

        public string Query { get; set; }

        /// <summary>
        /// Set the types of documents to include in the search results. Use SearchScopes.* contants. All documents are included by default.
        /// </summary>
        public void SetScope(string[] scopes)
        {
            if (scopes == null || scopes.Length == 0)
            {
                Scope = null;
                return;
            }
            Scope = string.Join(",", scopes);
        }

        protected override string GetUrl()
        {
            return string.Format("{0}/search", Version);
        }
    }
}
