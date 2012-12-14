using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class GetUserProjectListRequest
    {
        public GetUserProjectListRequest(int? offset = null, int? limit = null, SortDirection sortDir = SortDirection.Asc, ProjectsSortByParameters sortBy = ProjectsSortByParameters.Id, string name = null)
        {
            Offset = offset ?? 0;
            Limit = limit ?? 10;
            SortDir = sortDir;
            SortBy = sortBy;
            Name = name ?? string.Empty;
        }

        public int? Offset { get; set; }
        public int? Limit { get; set; }
        public SortDirection SortDir { get; set; }
        public ProjectsSortByParameters SortBy { get; set; }
        public string Name { get; set; }
    }
}
