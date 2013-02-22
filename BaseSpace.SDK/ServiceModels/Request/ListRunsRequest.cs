using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class ListRunsRequest : AbstractResourceListRequest<ListRunsResponse, RunSortByParameters>
    {
		public string Statuses { get; set; }

        public string InstrumentType { get; set; }

		protected override string GetUrl()
		{
			var urlWithParameters = AddDefaultQueryParameters(string.Format("{0}/users/current/runs", Version), Offset,
									 Limit, SortDir);
			if (SortBy.HasValue)
			{
				urlWithParameters = string.Format("{0}&{1}={2}", urlWithParameters, QueryParameters.SortBy, SortBy);
			}
			if (!string.IsNullOrEmpty(Statuses))
			{
				urlWithParameters = string.Format("{0}&{1}={2}", urlWithParameters, RunSortByParameters.Statuses, Statuses);
			}
			return urlWithParameters;
		}

		private static string AddDefaultQueryParameters(string relativeUrl, int? offset, int? limit, SortDirection? sortDir)
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
