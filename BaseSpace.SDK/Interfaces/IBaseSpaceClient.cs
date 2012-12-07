using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Illumina.BaseSpace.SDK.Models;

namespace Illumina.BaseSpace.SDK
{
    public interface IBaseSpaceClient
    {
        Task<GetUserResponse> GetUserAsync(GetUserRequest request, IRequestOptions options);

        GetUserResponse GetUser(GetUserRequest request, IRequestOptions options);

        Task<GetRunResponse> GetRunAsync(GetRunRequest request, IRequestOptions options);
        GetRunResponse GetRun(GetRunRequest request, IRequestOptions options);

        Task<GetProjectResponse> GetProjectAsync(GetProjectRequest request, IRequestOptions options);
        GetProjectResponse GetProject(GetProjectRequest request, IRequestOptions options);

        Task<GetProjectApiListResponse> ListProjects(GetProjectListRequest request, IRequestOptions options);

        void SetDefaultRequestOptions(IRequestOptions options);
    }
}
