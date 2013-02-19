using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class ListRunsRequest : AbstractResourceListRequest<RunSortByParameters>
    {
		public string Statuses { get; set; }

		public bool IncludeLocked { get; set; }

		public string ValidInstrumentType { get; set; }
    }
}
