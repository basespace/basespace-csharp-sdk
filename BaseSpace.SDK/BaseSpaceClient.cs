using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK
{
    public class BaseSpaceClient : IBaseSpaceClient
    {
        private static readonly IClientSettings defaultSettings = new BaseSpaceClientSettings();
        
        public BaseSpaceClient(string authCode)
            : this(new RequestOptions(){AuthCode = authCode, RetryAttempts = defaultSettings.RetryAttempts, BaseUrl = defaultSettings.BaseSpaceApiUrl})
        {

        }

        public BaseSpaceClient(IClientSettings settings, IRequestOptions defaultOptions = null) : this(settings, new JsonWebClient(settings), defaultOptions)
        {
        }

        public BaseSpaceClient(IRequestOptions options)
            : this(defaultSettings, new JsonWebClient(defaultSettings, options), options)
        {
            
        }

        public BaseSpaceClient(IClientSettings settings, IWebClient client, IRequestOptions defaultOptions = null)
        {
            if (settings == null || client == null)
            {
                throw new ArgumentNullException("settings");
            }
            ClientSettings = settings;
            WebClient = client;
            SetDefaultRequestOptions(defaultOptions);
        }

        protected IClientSettings ClientSettings { get; set; }

        protected IWebClient WebClient { get; set; }

        public void SetDefaultRequestOptions(IRequestOptions options)
        {
            
            WebClient.SetDefaultRequestOptions(options);
        }

        #region Users
        public Task<GetUserResponse> GetUserAsync(GetUserRequest request, IRequestOptions options = null)
        {
            return WebClient.SendAsync<GetUserResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }

        public GetUserResponse GetUser(GetUserRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<GetUserResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }
        #endregion


        #region Runs
        public Task<GetRunResponse> GetRunAsync(GetRunRequest request, IRequestOptions options = null)
        {
            return WebClient.SendAsync<GetRunResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }

        public GetRunResponse GetRun(GetRunRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<GetRunResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }

        public Task<ListRunsResponse> ListRunsAsync(ListRunsRequest request, IRequestOptions options)
        {
            return WebClient.SendAsync<ListRunsResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }

        public ListRunsResponse ListRuns(ListRunsRequest request, IRequestOptions options)
        {
            return WebClient.Send<ListRunsResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }
        #endregion


        #region Projects
        public Task<GetProjectResponse> GetProjectAsync(GetProjectRequest request, IRequestOptions options = null)
        {
            return WebClient.SendAsync<GetProjectResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }

        public GetProjectResponse GetProject(GetProjectRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<GetProjectResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }

        public Task<ListProjectsResponse> ListProjectsAsync(ListProjectsRequest request, IRequestOptions options)
        {
            return WebClient.SendAsync<ListProjectsResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }

        public ListProjectsResponse ListProjects(ListProjectsRequest request, IRequestOptions options)
        {
            return WebClient.Send<ListProjectsResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }
        #endregion


        #region AppSessions
        public Task<GetAppSessionResponse> GetAppSessionAsync(GetAppSessionRequest request, IRequestOptions options = null)
        {
            return WebClient.SendAsync<GetAppSessionResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }

        public GetAppSessionResponse GetAppSession(GetAppSessionRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<GetAppSessionResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }
        #endregion


        #region Samples
        public Task<GetSampleResponse> GetSampleAsync(GetSampleRequest request, IRequestOptions options = null)
        {
            return WebClient.SendAsync<GetSampleResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }

        public GetSampleResponse GetSample(GetSampleRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<GetSampleResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }

        public Task<ListSamplesResponse> ListSamplesAsync(ListSamplesRequest request, IRequestOptions options)
        {
            return WebClient.SendAsync<ListSamplesResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }

        public ListSamplesResponse ListSamples(ListSamplesRequest request, IRequestOptions options)
        {
            return WebClient.Send<ListSamplesResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }
        #endregion
        

        #region AppResults
        public Task<GetAppResultResponse> GetAppResultAsync(GetAppResultRequest request, IRequestOptions options = null)
        {
            return WebClient.SendAsync<GetAppResultResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }

        public GetAppResultResponse GetAppResult(GetAppResultRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<GetAppResultResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }

        public Task<ListAppResultsResponse> ListAppResultsAsync(ListAppResultsRequest request, IRequestOptions options)
        {
            return WebClient.SendAsync<ListAppResultsResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }

        public ListAppResultsResponse ListAppResults(ListAppResultsRequest request, IRequestOptions options)
        {
            return WebClient.Send<ListAppResultsResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }
        #endregion


        #region Genomes
        
        #endregion


        #region Files
        
        #endregion


        #region Variants
        
        #endregion


        #region Coverage
        
        #endregion
    }
}