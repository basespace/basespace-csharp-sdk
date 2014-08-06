using System;
using Illumina.BaseSpace.SDK.Types;
using System.ComponentModel;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class ListRunsRequest : AbstractResourceListRequest<ListRunsResponse, RunSortByParameters>
    {
        [Description("The run statuses to filter by. Valid values include: New, Ready, Running, Paused, Stopped, Uploading, PendingAnalysis, Analyzing, Complete, Failed, NeedsAttention. Multiple values may be provided separated by commas.")]
        public string Statuses { get; set; }

        [Description("The instrument type to filter by")]
        public string InstrumentType { get; set; }

        [Description("If true, will include runs locks by other users and/or on other instruments")]
        public bool IncludeLocked { get; set; }

        [Description("The reagent barcode to filter by. To request empty barcodes, provide an empty string")]
        public string ReagentBarcode { get; set; }

        [Description("The flowcell barcode to filter by. To request empty barcodes, provide an empty string")]
        public string FlowcellBarcode { get; set; }

        protected override string GetUrl()
        {
            return String.Format("{0}/users/current/runs", Version);
        }
    }
}
