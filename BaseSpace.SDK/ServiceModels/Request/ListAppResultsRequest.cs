using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class ListAppResultsRequest : AbstractResourceListRequest<AppResultSortByParameters>
    {
        /// <summary>
        /// List AppResults within a project
        /// </summary>
        /// <param name="projectId">Project Id</param>
        public ListAppResultsRequest(string projectId) : base(projectId)
        {
        }
    }
}
