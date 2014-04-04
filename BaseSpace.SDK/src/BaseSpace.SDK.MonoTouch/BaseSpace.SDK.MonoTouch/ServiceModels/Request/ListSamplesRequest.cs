using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class ListSamplesRequest : AbstractResourceListRequest<SamplesSortByParameters>
    {
        /// <summary>
        /// List samples within a project
        /// </summary>
        /// <param name="projectId">Project Id</param>
        public ListSamplesRequest(string projectId)
        {
            ProjectId = projectId;
        }

        public string ProjectId { get; set; }
    }
}
