using System;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class GetRunSequencingStatsRequest : AbstractRequest<GetRunSequencingStatsResponse>
    {
        public RunCompact Run { get; private set; }

        public GetRunSequencingStatsRequest()
        {
            HttpMethod = HttpMethods.GET;
        }

        public GetRunSequencingStatsRequest(RunCompact run)
        {
            if (run == null)
            {
                throw new ArgumentNullException("run");
            }

            Run = run;
        }

        protected override string GetUrl()
        {
            return string.Format("{0}/sequencingstats", Run.Href);
        }
    }
}
