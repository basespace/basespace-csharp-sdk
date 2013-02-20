using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class ListRunFilesRequest : AbstractResourceListRequest<RunFilesSortByParameters>
    {
        /// <summary>
        /// List files belonging to a run
        /// </summary>
        /// <param name="runId">Run Id</param>
        public ListRunFilesRequest(string runId)
			: base(runId)
        {
        }

        public ListRunFilesRequest() { }

       public string Extensions { get; set; }
    }
}

