using System;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class ListRunsRequest : AbstractResourceListRequest<ListRunsResponse, RunSortByParameters>
    {
		public string Statuses { get; set; }

        public string InstrumentType { get; set; }

        protected override string GetUrl()
        {
            return String.Format("{0}/users/current/runs", Version);
        }
	}
}
