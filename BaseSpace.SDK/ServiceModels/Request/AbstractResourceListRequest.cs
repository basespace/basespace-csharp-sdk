using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public abstract class AbstractResourceListRequest<TResult, TSortFieldEnumType> : AbstractResourceRequest<TResult>
		where TResult : class 
		where TSortFieldEnumType : struct
    {
		protected AbstractResourceListRequest()
		{
		}

		protected AbstractResourceListRequest(string id)
			: base(id)
		{
		}

        public int? Offset { get; set; }

        public int? Limit { get; set; }

        public SortDirection? SortDir { get; set; }

        public TSortFieldEnumType? SortBy { get; set; }

		protected static string AddDefaultQueryParameters(string relativeUrl, int? offset, int? limit, SortDirection? sortDir)
		{
			var url = (offset.HasValue || limit.HasValue || sortDir.HasValue) && relativeUrl.Contains("?") ? relativeUrl : string.Format("{0}?", relativeUrl);
			if (offset.HasValue)
			{
				url = string.Format("{0}&{1}={2}", url, QueryParameters.Offset, offset.Value);
			}
			if (sortDir.HasValue)
			{
				url = string.Format("{0}&{1}={2}", url, QueryParameters.SortDir, sortDir.Value);
			}
			if (limit.HasValue)
			{
				url = string.Format("{0}&{1}={2}", url, QueryParameters.Limit, limit.Value);
			}
			return url;
		}
    }
}
