using System;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class ListRunFilesRequest : ListFilesRequest<ListRunFilesResponse, RunFilesSortByParameters>
    {
        /// <summary>
        /// List files belonging to a run
        /// </summary>
        /// <param name="runId">Run Id</param>
        public ListRunFilesRequest(string runId)
			: base(runId)
        {
		}

        protected override string GetUrl()
	    {
		    var url = BuildUrl(String.Format("{0}/runs/{1}/files", Version, Id));

		    return UpdateUrl(Extensions, url);
	    }
	}
}

