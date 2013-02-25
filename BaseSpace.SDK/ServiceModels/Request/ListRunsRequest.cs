using System;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class ListRunsRequest : AbstractResourceListRequest<ListRunsResponse, RunSortByParameters>
    {
		public string Statuses { get; set; }

        public string InstrumentType { get; set; }

		protected override bool HasFilters()
		{
			return base.HasFilters() || (Statuses != null) || (InstrumentType != null);
		}

		protected override string GetUrl()
		{
			return BuildUrl(String.Format("{0}/users/current/runs", Version));
		}

		protected override string BuildUrl(string relativeUrl)
		{
			var url = base.BuildUrl(relativeUrl);

			url = UpdateUrl(Statuses, relativeUrl);
			return UpdateUrl(InstrumentType, url);
		}
	}
}
