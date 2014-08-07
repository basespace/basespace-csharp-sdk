using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.Types;
using Common.Logging;
using System.Net;
using System.IO;
using ServiceStack.Text;
using ServiceStack.ServiceClient.Web;

namespace Illumina.BaseSpace.SDK
{
    public class BaseSpaceClient : IBaseSpaceClient
    {
        private static readonly IClientSettings defaultSettings = new BaseSpaceClientSettings();
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

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

        public Task<PostProjectResponse> CreateProjectAsync(PostProjectRequest request, IRequestOptions options = null)
        {
            return WebClient.SendAsync<PostProjectResponse>(HttpMethods.POST, request.BuildUrl(ClientSettings.Version), request, options);
        }

        public PostProjectResponse CreateProject(PostProjectRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<PostProjectResponse>(HttpMethods.POST, request.BuildUrl(ClientSettings.Version), request, options);
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

        public Task<UpdateAppSessionResponse> UpdateAppSessionAsync(UpdateAppSessionRequest request, IRequestOptions options = null)
        {
            return WebClient.SendAsync<UpdateAppSessionResponse>(HttpMethods.POST, request.BuildUrl(ClientSettings.Version), request, options);
        }

        public UpdateAppSessionResponse UpdateAppSession(UpdateAppSessionRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<UpdateAppSessionResponse>(HttpMethods.POST, request.BuildUrl(ClientSettings.Version), request, options);
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

        public Task<PostAppResultResponse> CreateAppResultAsync(PostAppResultRequest request, IRequestOptions options = null)
        {
            return WebClient.SendAsync<PostAppResultResponse>(HttpMethods.POST, request.BuildUrl(ClientSettings.Version), request, options);
        }

        public PostAppResultResponse CreateAppResult(PostAppResultRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<PostAppResultResponse>(HttpMethods.POST, request.BuildUrl(ClientSettings.Version), request, options);
        }
        #endregion


        #region Genomes
        public Task<GetGenomeResponse> GetGenomeAsync(GetGenomeRequest request, IRequestOptions options = null)
        {
            return WebClient.SendAsync<GetGenomeResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }

        public GetGenomeResponse GetGenome(GetGenomeRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<GetGenomeResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }

        public Task<ListGenomeResponse> ListGenomesAsync(ListGenomeRequest request, IRequestOptions options)
        {
            return WebClient.SendAsync<ListGenomeResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }

        public ListGenomeResponse ListGenomes(ListGenomeRequest request, IRequestOptions options)
        {
            return WebClient.Send<ListGenomeResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }
        #endregion


        #region Files
        public Task<ListRunFilesResponse> ListRunFilesAsync(ListRunFilesRequest request, IRequestOptions options)
        {
            return WebClient.SendAsync<ListRunFilesResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }

        public ListRunFilesResponse ListRunFiles(ListRunFilesRequest request, IRequestOptions options)
        {
            return WebClient.Send<ListRunFilesResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }

        public Task<ListSampleFilesResponse> ListSampleFilesAsync(ListSampleFilesRequest request, IRequestOptions options)
        {
            return WebClient.SendAsync<ListSampleFilesResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }

        public ListSampleFilesResponse ListSampleFiles(ListSampleFilesRequest request, IRequestOptions options)
        {
            return WebClient.Send<ListSampleFilesResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }

        public Task<ListAppResultFilesResponse> ListAppResultFilesAsync(ListAppResultFilesRequest request, IRequestOptions options)
        {
            return WebClient.SendAsync<ListAppResultFilesResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }

        public ListAppResultFilesResponse ListAppResultFiles(ListAppResultFilesRequest request, IRequestOptions options)
        {
            return WebClient.Send<ListAppResultFilesResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }
        #endregion


        #region Variants
        public Task<GetVariantHeaderResponse> GetVariantHeaderAsync(GetVariantHeaderRequest request, IRequestOptions options = null)
        {
            return WebClient.SendAsync<GetVariantHeaderResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }

        public GetVariantHeaderResponse GetVariantHeader(GetVariantHeaderRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<GetVariantHeaderResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }

        public Task<ListVariantsResponse> ListVariantsAsync(ListVariantsRequest request, IRequestOptions options = null)
        {
            return WebClient.SendAsync<ListVariantsResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }

        public ListVariantsResponse ListVariants(ListVariantsRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<ListVariantsResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }
        #endregion


        #region Coverage
        public Task<GetCoverageResponse> GetCoverageAsync(GetCoverageRequest request, IRequestOptions options = null)
        {
            return WebClient.SendAsync<GetCoverageResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }

        public GetCoverageResponse GetCoverage(GetCoverageRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<GetCoverageResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }

        public Task<GetCoverageMetadataResponse> GetCoverageMetadataAsync(GetCoverageMetadataRequest request, IRequestOptions options = null)
        {
            return WebClient.SendAsync<GetCoverageMetadataResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }

        public GetCoverageMetadataResponse GetCoverageMetadata(GetCoverageMetadataRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<GetCoverageMetadataResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }
        #endregion

        #region OAuth
        public OAuthDeviceAuthResponse BeginOAuthDeviceAuth(OAuthDeviceAuthRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<OAuthDeviceAuthResponse>(HttpMethods.POST, request.BuildUrl(ClientSettings.Version), request, options);
        }

        public OAuthDeviceAccessTokenResponse FinishOAuthDeviceAuth (OAuthDeviceAccessTokenRequest request, IRequestOptions options = null)
        {
            try
            {
                return WebClient.Send<OAuthDeviceAccessTokenResponse> (HttpMethods.POST, request.BuildUrl (ClientSettings.Version), request, options);
            }
            catch (BaseSpaceException bex)
            {
                if(bex.InnerException != null && bex.InnerException.GetType() == typeof(WebServiceException))
                {
                    var wsex = (WebServiceException)bex.InnerException;
                    return wsex.ResponseBody.FromJson<OAuthDeviceAccessTokenResponse>();
                }
            }
            return null;
        }
        #endregion
    }
}