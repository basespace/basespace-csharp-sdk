using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Illumina.BaseSpace.SDK.ServiceModels;

namespace Illumina.BaseSpace.SDK
{
    public interface IBaseSpaceClient
    {
        Task<GetUserResponse> GetUserAsync(GetUserRequest request, IRequestOptions options = null);
        GetUserResponse GetUser(GetUserRequest request, IRequestOptions options = null);

        Task<GetRunResponse> GetRunAsync(GetRunRequest request, IRequestOptions options = null);
        GetRunResponse GetRun(GetRunRequest request, IRequestOptions options = null);

        Task<GetProjectResponse> GetProjectAsync(GetProjectRequest request, IRequestOptions options = null);
        GetProjectResponse GetProject(GetProjectRequest request, IRequestOptions options = null);

        Task<ListProjectsResponse> ListProjectsAsync(ListProjectsRequest request, IRequestOptions options = null);
        ListProjectsResponse ListProjects(ListProjectsRequest request, IRequestOptions options = null);

        void SetDefaultRequestOptions(IRequestOptions options = null);
    }
}
