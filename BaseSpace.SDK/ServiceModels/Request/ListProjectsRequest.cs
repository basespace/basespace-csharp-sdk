using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class ListProjectsRequest : AbstractResourceListRequest<ProjectsSortByParameters>
    {
        public string Name { get; set; }
    }
}
