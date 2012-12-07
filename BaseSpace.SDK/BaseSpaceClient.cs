using System;
using System.Threading.Tasks;
using Illumina.BaseSpace.SDK.Models;

namespace Illumina.BaseSpace.SDK
{
    public class BaseSpaceClient : IBaseSpaceClient
    {
        public BaseSpaceClient(IClientSettings settings) : this(settings, new JsonWebClient(settings))
        {
        }

        public BaseSpaceClient(IClientSettings settings, IWebClient client)
        {
            if (settings == null || client == null)
            {
                throw new ArgumentNullException("settings");
            }
            ClientSettings = settings;
            WebClient = client;
        }

        protected IClientSettings ClientSettings { get; set; }

        protected IWebClient WebClient { get; set; }
        
        public void SetDefaultRequestOptions(IRequestOptions options)
        {
            WebClient.SetDefaultRequestOptions(options);
        }

        public Task<GetUserResponse> GetUserAsync(GetUserRequest request, IRequestOptions options = null)
        {
            return WebClient.SendAsync<GetUserResponse>(HttpMethods.GET, request.GenerateUrl(ClientSettings.Version), null, options);
        }

        public GetUserResponse GetUser(GetUserRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<GetUserResponse>(HttpMethods.GET, request.GenerateUrl(ClientSettings.Version), null, options);
        }

        public Task<GetRunResponse> GetRunAsync(GetRunRequest request, IRequestOptions options = null)
        {
            return WebClient.SendAsync<GetRunResponse>(HttpMethods.GET, request.GenerateUrl(ClientSettings.Version), null, options);
        }

        public GetRunResponse GetRun(GetRunRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<GetRunResponse>(HttpMethods.GET, request.GenerateUrl(ClientSettings.Version), null, options);
        }

        public Task<GetProjectResponse> GetProjectAsync(GetProjectRequest request, IRequestOptions options = null)
        {
            return WebClient.SendAsync<GetProjectResponse>(HttpMethods.GET, request.GenerateUrl(ClientSettings.Version), null, options);
        }

        public GetProjectResponse GetProject(GetProjectRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<GetProjectResponse>(HttpMethods.GET, request.GenerateUrl(ClientSettings.Version), null, options);
        }

        public Task<GetProjectApiListResponse> ListProjects(GetProjectListRequest request, IRequestOptions options)
        {
            return WebClient.SendAsync<GetProjectApiListResponse>(HttpMethods.GET, request.GenerateUrl(ClientSettings.Version), null, options);
        }

    }
}