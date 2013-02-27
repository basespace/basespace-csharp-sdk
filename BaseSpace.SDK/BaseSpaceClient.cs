using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.ServiceModels.Request;
using Illumina.BaseSpace.SDK.ServiceModels.Response;
using Illumina.BaseSpace.SDK.Types;
using ServiceStack.ServiceClient.Web;
using ServiceStack.Text;

namespace Illumina.BaseSpace.SDK
{
    public class BSWebClient : WebClient
    {
        //TODO: Doesnt seem right? Need refactor?
       // const int CONNECTION_LIMIT = 16;
        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address) as HttpWebRequest;

            if (request != null && request.ServicePoint.ConnectionLimit < 20)
                request.ServicePoint.ConnectionLimit = 10000;  //Note: Is this changing global value?

            return request;
        }
    }

    public class BaseSpaceClient : IBaseSpaceClient
    {
        protected static readonly IClientSettings defaultSettings = new BaseSpaceClientSettings();
        
        
        public BaseSpaceClient(string accessToken)
			: this(new RequestOptions { Authentication = new OAuth2Authentication(accessToken), RetryAttempts = defaultSettings.RetryAttempts, BaseUrl = defaultSettings.BaseSpaceApiUrl })
        {

        }

        public BaseSpaceClient(IClientSettings settings, IRequestOptions defaultOptions = null) 
			: this(settings, new JsonWebClient(settings), defaultOptions)
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
        public GetUserResponse GetUser(GetUserRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<GetUserResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }
        #endregion


        #region Runs
        public GetRunResponse GetRun(GetRunRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<GetRunResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }

        public ListRunsResponse ListRuns(ListRunsRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<ListRunsResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), request, options);
        }
        #endregion


        #region Projects
        public GetProjectResponse GetProject(GetProjectRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<GetProjectResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }

        public ListProjectsResponse ListProjects(ListProjectsRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<ListProjectsResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), request, options);
        }

        public CreateProjectResponse CreateProject(CreateProjectRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<CreateProjectResponse>(HttpMethods.POST, request.BuildUrl(ClientSettings.Version), request, options);
        }
        #endregion


        #region AppSessions
        public GetAppSessionResponse GetAppSession(GetAppSessionRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<GetAppSessionResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }

        public UpdateAppSessionResponse UpdateAppSession(UpdateAppSessionRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<UpdateAppSessionResponse>(HttpMethods.POST, request.BuildUrl(ClientSettings.Version), request, options);
        }
        #endregion


        #region Samples
        public GetSampleResponse GetSample(GetSampleRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<GetSampleResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }

        public ListSamplesResponse ListSamples(ListSamplesRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<ListSamplesResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), request, options);
        }
        #endregion
        

        #region AppResults
        public GetAppResultResponse GetAppResult(GetAppResultRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<GetAppResultResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }

        public ListAppResultsResponse ListAppResults(ListAppResultsRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<ListAppResultsResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), request, options);
        }

        public CreateAppResultResponse CreateAppResult(CreateAppResultRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<CreateAppResultResponse>(HttpMethods.POST, request.BuildUrl(ClientSettings.Version), request, options);
        }
        #endregion


        #region Genomes
        public GetGenomeResponse GetGenome(GetGenomeRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<GetGenomeResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }

        public ListGenomeResponse ListGenomes(ListGenomeRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<ListGenomeResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), request, options);
        }
        #endregion


        #region Files
        public ListRunFilesResponse ListRunFiles(ListRunFilesRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<ListRunFilesResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), request, options);
        }

        public ListSampleFilesResponse ListSampleFiles(ListSampleFilesRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<ListSampleFilesResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), request, options);
        }

        public ListAppResultFilesResponse ListAppResultFiles(ListAppResultFilesRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<ListAppResultFilesResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), request, options);
        }

        public GetFileInformationResponse GetFilesInformation(GetFileInformationRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<GetFileInformationResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }

        public Types.File UploadFileToAppResult(System.IO.FileInfo sourceFileInfo, UploadFileToAppResultRequest toAppResultRequest,
                                                                   IRequestOptions options = null) 
        {
            var fileUploadClient = new FileUpload(WebClient, ClientSettings, options ?? WebClient.DefaultRequestOptions);
            return fileUploadClient.UploadFile<UploadFileToAppResultRequest>(sourceFileInfo, toAppResultRequest.Id,
                                                                             toAppResultRequest.ResourceIdentifierInUri,
                                                                             toAppResultRequest.Directory);
        }
        #endregion


        #region Variants
        public GetVariantHeaderResponse GetVariantHeader(GetVariantHeaderRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<GetVariantHeaderResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }

        public ListVariantsResponse ListVariants(ListVariantsRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<ListVariantsResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), request, options);
        }
        #endregion


        #region Coverage
        public GetCoverageResponse GetCoverage(GetCoverageRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<GetCoverageResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
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
			return WebClient.Send<OAuthDeviceAccessTokenResponse>(HttpMethods.POST, request.BuildUrl(ClientSettings.Version), request, options);
		}

        public OAuthV2AccessTokenResponse GetOAuthAccessToken(OAuthV2AccessTokenRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<OAuthV2AccessTokenResponse>(HttpMethods.POST, request.BuildUrl(ClientSettings.Version), request, options);
        }

		#endregion

        #region FileDownload
        public FileContentRedirectMetaResponse GetFileContentUrl(FileContentRedirectMetaRequest request, IRequestOptions options = null)
        {
            return WebClient.Send<FileContentRedirectMetaResponse>(HttpMethods.GET, request.BuildUrl(ClientSettings.Version), null, options);
        }

        public void DownloadFileById(string fileId, Stream stream, CancellationToken token = new CancellationToken())
        {
            var command = new DownloadFileCommand(this, fileId, stream, ClientSettings, token);
            command.FileDownloadProgressChanged += command_FileDownloadProgressChanged;

            command.Execute();
        }

        public void DownloadFile(FileCompact file, Stream stream, CancellationToken token = new CancellationToken())
        {
            var command = new DownloadFileCommand(this, file, stream, ClientSettings, token);
            command.FileDownloadProgressChanged += command_FileDownloadProgressChanged;

            command.Execute();
        }
        public event FileDownloadProgressChangedEventHandler FileDownloadProgressChanged;

        protected void OnFileDownloadProgressChanged(FileDownloadProgressChangedEventArgs e)
        {
            if (FileDownloadProgressChanged != null)
            {
                FileDownloadProgressChanged(this, e);
            }
        }

        private void command_FileDownloadProgressChanged(object sender, FileDownloadProgressChangedEventArgs e)
        {
            OnFileDownloadProgressChanged(e);
        }
        #endregion
    }
}