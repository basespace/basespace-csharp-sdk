using System;
using Illumina.BaseSpace.SDK.ServiceModels.Response;
using Illumina.BaseSpace.SDK.Types;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class ListSampleLibrariesFromRunRequest : AbstractResourceListRequest<ListSampleLibrariesResponse, LibrariesSortFields>
    {
        /// <summary>
        /// List samples libraries within a run
        /// </summary>
        /// <param name="runId">Run Id</param>
        public ListSampleLibrariesFromRunRequest(string runId)
			: base(runId)
        {
        }

        protected override string GetUrl()
        {
            return String.Format("{0}/runs/{1}/samplelibraries", Version, Id);
        }
    }
}
