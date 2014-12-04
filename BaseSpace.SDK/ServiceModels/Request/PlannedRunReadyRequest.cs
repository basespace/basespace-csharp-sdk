using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class PlannedRunReadyRequest : AbstractRequest<PlannedRunReadyResponse>
    {
        public PlannedRunReadyRequest(string plannedRunId, bool isReady)
        {
            HttpMethod = HttpMethods.PUT;
            PlannedRunId = plannedRunId;
            Ready = isReady;
        }
        public string PlannedRunId {get; set;}
        public bool Ready { get; set; }

        protected override string GetUrl()
        {
            return string.Format("{0}/plannedruns/{1}/ready", Version, PlannedRunId);
        }
    }
}