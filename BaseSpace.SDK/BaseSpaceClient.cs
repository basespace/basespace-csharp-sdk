using System;
using System.IO;
using System.Net;
using System.Threading;
using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK
{
    public class BaseSpaceClient : IBaseSpaceClient
    {
        public BaseSpaceClient(string accessToken)
            : this(new BaseSpaceClientSettings { Authentication = new OAuth2Authentication(accessToken) })
        {
        }

        public BaseSpaceClient(IClientSettings settings, IRequestOptions defaultOptions = null)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }

            Settings = settings;
            WebClient = new JsonWebClient(settings, defaultOptions);
        }

        public IClientSettings Settings { get; private set; }

        public IWebProxy WebProxy
        {
            get { return WebClient.WebProxy; }
            set { WebClient.WebProxy = value; }
        }

        protected IWebClient WebClient { get; private set; }

        #region Users
        public GetUserResponse GetUser(GetUserRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }
        #endregion

        #region Runs
        public GetRunResponse GetRun(GetRunRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }

        public ListRunsResponse ListRuns(ListRunsRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }
        #endregion

        #region Projects
        public GetProjectResponse GetProject(GetProjectRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }

        public ListProjectsResponse ListProjects(ListProjectsRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }

        public CreateProjectResponse CreateProject(CreateProjectRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }
        #endregion

        #region AppSessions
        public GetAppSessionResponse GetAppSession(GetAppSessionRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }

        public ListCurrentAppSessionsResponse ListCurrentAppSessions(ListCurrentAppSessionsRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }

        public UpdateAppSessionResponse UpdateAppSession(UpdateAppSessionRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }

        public CreateAppSessionLogsResponse CreateAppSessionLogs(CreateAppSessionLogsRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }

        #endregion

        #region Samples
        public GetSampleResponse GetSample(GetSampleRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }

        public ListSamplesResponse ListSamples(ListSamplesRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }
        #endregion

        #region AppResults
        public GetAppResultResponse GetAppResult(GetAppResultRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }

        public ListAppResultsResponse ListAppResults(ListAppResultsRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }

        public CreateAppResultResponse CreateAppResult(CreateAppResultRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }

        public ListAppResultsResponse ListAppResultByAppSession(ListAppResultsByAppSessionRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }
        #endregion

        #region Genomes
        public GetGenomeResponse GetGenome(GetGenomeRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }

        public ListGenomeResponse ListGenomes(ListGenomeRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }

        public ListGenomeAnnotationsResponse ListGenomeAnnotations(ListGenomeAnnotationsRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }

        #endregion

        #region Files
        public ListRunFilesResponse ListRunFiles(ListRunFilesRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }

        public ListSampleFilesResponse ListSampleFiles(ListSampleFilesRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }

        public ListAppResultFilesResponse ListAppResultFiles(ListAppResultFilesRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }

        public GetFileInformationResponse GetFilesInformation(GetFileInformationRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }

        public UploadFileToAppResultResponse UploadFileToAppResult(UploadFileToAppResultRequest toAppResultRequest, IRequestOptions options = null)
        {
            var fileUploadClient = new FileUpload(WebClient, Settings, options ?? WebClient.DefaultRequestOptions);
            return fileUploadClient.UploadFile(toAppResultRequest);
        }
        #endregion

        #region FileSet
        public ListGenomeFileSetsResponse ListFileSets(ListGenomeFileSetsRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }

        public ListGenomeAnnotationFileSetsResponse ListFileSets(ListGenomeAnnotationFileSetsRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }

        public ListFileSetFilesResponse ListFiles(ListFileSetFilesRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }

        public UploadFileToFileSetResponse UploadFileToFileSet(UploadFileToFileSetRequest request, IRequestOptions options = null)
        {
            var fileUploadClient = new FileUpload(WebClient, Settings, options ?? WebClient.DefaultRequestOptions);
            return fileUploadClient.UploadFile(request);
        }
        #endregion

        #region Variants
        public GetVariantHeaderResponse GetVariantHeader(GetVariantHeaderRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }

        public ListVariantsResponse ListVariants(ListVariantsRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }
        #endregion

        #region Coverage
        public GetCoverageResponse GetCoverage(GetCoverageRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }

        public GetCoverageMetadataResponse GetCoverageMetadata(GetCoverageMetadataRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }
        #endregion

        #region OAuth
        public OAuthDeviceAuthResponse BeginOAuthDeviceAuth(OAuthDeviceAuthRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }

        public OAuthDeviceAccessTokenResponse FinishOAuthDeviceAuth(OAuthDeviceAccessTokenRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }

        public OAuthV2AccessTokenResponse GetOAuthAccessToken(OAuthV2AccessTokenRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }

        #endregion

        #region FileDownload
        public FileContentRedirectMetaResponse GetFileContentUrl(FileContentRedirectMetaRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }

        public void DownloadFile(string fileId, Stream stream, CancellationToken token = new CancellationToken())
        {
            var command = new DownloadFileCommand(this, fileId, stream, Settings, token, WebProxy);
            command.FileDownloadProgressChanged += command_FileDownloadProgressChanged;

            command.Execute();
        }

        public void DownloadFile(FileCompact file, Stream stream, CancellationToken token = new CancellationToken())
        {
            var command = new DownloadFileCommand(this, file, stream, Settings, token, WebProxy);
            command.FileDownloadProgressChanged += command_FileDownloadProgressChanged;

            command.Execute();
        }

        public void DownloadFile(FileCompact file, string filePath, CancellationToken token = new CancellationToken())
        {
            var command = new DownloadFileCommand(this, file, filePath, Settings, token);
            command.FileDownloadProgressChanged += command_FileDownloadProgressChanged;

            command.Execute();
        }

        public void DownloadFile(FileCompact file, string filePath, int maxThreadCount,
            CancellationToken token = new CancellationToken())
        {
            var command = new DownloadFileCommand(this, file, filePath, Settings, token, threadCount: maxThreadCount);
            command.FileDownloadProgressChanged += command_FileDownloadProgressChanged;
            command.Execute();
        }

        public void DownloadFile(FileCompact file, string filePath, int maxChunkSize, int maxThreadCount,
                                 CancellationToken token = new CancellationToken())
        {
            var command = new DownloadFileCommand(this, file, filePath, Settings, maxThreadCount, maxChunkSize, token);
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

        #region ApiMeta
        public GetApiMetaResponse GetApiMeta(GetApiMetaRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }
        #endregion

        #region Properties

        /// <summary>
        /// Retrieve list of properties for a resource. Resulting list may require paging.
        /// </summary>
        public ListPropertiesResponse ListPropertiesForResource(ListPropertiesRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }

        /// <summary>
        /// Retrieve one property given its parent resource and name
        /// </summary>
        public GetPropertyResponse GetPropertyForResource(GetPropertyRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }

        /// <summary>
        /// Retrieve list of items for a multi-value property (property having a type ending in '[]'). Resulting list may require paging.
        /// </summary>
        public ListPropertyItemsResponse ListPropertyItems(ListPropertyItemsRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }

        /// <summary>
        /// Add or modify properties associated with a resource. The list of modified properties will be returned.
        /// </summary>
        public SetPropertiesResponse SetPropertiesForResource(SetPropertiesRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }

        /// <summary>
        /// Delete a property given its parent resource and name.
        /// </summary>
        public DeletePropertyResponse DeletePropertyForResource(DeletePropertyRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }
        #endregion

        #region Search
        public SearchResponse Search(SearchRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }
        #endregion

        #region Billing
        public GetPurchaseResponse GetPurchase(GetPurchaseRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }

        public CreatePurchaseResponse CreatePurchase(CreatePurchaseRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }

        public CreatePurchaseRefundResponse CreatePurchaseRefund(CreatePurchaseRefundRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }

        public ListPurchasedProductsResponse ListPurchasedProducts(ListPurchasedProductsRequest request, IRequestOptions options = null)
        {
            return WebClient.Send(request, options);
        }
        #endregion
    }
}