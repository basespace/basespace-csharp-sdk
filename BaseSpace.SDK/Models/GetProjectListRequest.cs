using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumina.BaseSpace.SDK.Models
{
    public class GetProjectListRequest : IBaseSpaceRequest
    {
        public GetProjectListRequest(int? offset = null, int? limit = null, SortDirection sortDir = SortDirection.Asc, ProjectsSortFields sortBy = ProjectsSortFields.Id, string name = null)
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
        public ProjectsSortFields SortBy { get; set; }
        public string Name { get; set; }

        public string GenerateUrl(string version)
        {
            return string.Format("{0}/users/current/projects?{1}&{2}&{3}&{4}&{5}", version, Offset, Limit, SortDir, SortBy, Name );
        }
    }
}
