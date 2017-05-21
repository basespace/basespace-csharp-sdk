using System.IO;
using System.Net;
using System.Threading;
using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.ServiceModels.Response;
using Illumina.BaseSpace.SDK.Types;
using System.Collections.Generic;

namespace Illumina.BaseSpace.SDK
{
    public interface IBaseSpaceClient
    {
        IWebProxy WebProxy { get; set; }

        GetUserResponse GetUser(GetUserRequest request, IRequestOptions options = null);

        GetAccessTokenDetailsResponse GetUserPermissions(GetAccessTokenDetailsRequest request, IRequestOptions options = null);

		GetRunResponse GetRun(GetRunRequest request, IRequestOptions options = null);

        GetRunSequencingStatsResponse GetRunSequencingStats(GetRunSequencingStatsRequest request, IRequestOptions options = null);

        ListRunsResponse ListRuns(ListRunsRequest request, IRequestOptions options = null);

        ListLibraryContainersResponse ListLibraryContainers(ListLibraryContainersRequest request, IRequestOptions options = null);

		GetProjectResponse GetProject(GetProjectRequest request, IRequestOptions options = null);

		ListProjectsResponse ListProjects(ListProjectsRequest request, IRequestOptions options = null);

		CreateProjectResponse CreateProject(CreateProjectRequest request, IRequestOptions options = null);

        ListAppSessionsResponse ListAppSessions(ListAppSessionsRequest request, IRequestOptions options = null);

		ProjectShareResponse CreateProjectShare(CreateProjectShareRequest request, IRequestOptions options = null);
				
		GetAppSessionResponse GetAppSession(GetAppSessionRequest request, IRequestOptions options = null);

        CreateAppSessionLogsResponse CreateAppSessionLogs(CreateAppSessionLogsRequest request, IRequestOptions options = null);

		GetSampleResponse GetSample(GetSampleRequest request, IRequestOptions options = null);

        ListSamplesResponse ListSamples(ListSamplesRequest request, IRequestOptions options = null);

        ListAppResultsResponse ListAppResults(ListAppResultsRequest request, IRequestOptions options = null);

        ListAppResultsResponse ListAppResultsFromAppSession(ListAppResultsFromAppSessionRequest request, IRequestOptions options = null);

		GetAppResultResponse GetAppResult(GetAppResultRequest request, IRequestOptions options = null);

        ListGenomeResponse ListGenomes(ListGenomeRequest request, IRequestOptions options = null);

        ListGenomeAnnotationsResponse ListGenomeAnnotations(ListGenomeAnnotationsRequest request, IRequestOptions options = null);

        ListGenomeFileSetsResponse ListFileSets(ListGenomeFileSetsRequest request, IRequestOptions options = null);

        ListGenomeAnnotationFileSetsResponse ListFileSets(ListGenomeAnnotationFileSetsRequest request, IRequestOptions options = null);

        ListFileSetFilesResponse ListFiles(ListFileSetFilesRequest request, IRequestOptions options = null);

        UploadFileToFileSetResponse UploadFileToFileSet(UploadFileToFileSetRequest request, IRequestOptions options = null);

		UpdateAppSessionResponse UpdateAppSession(UpdateAppSessionRequest request, IRequestOptions options = null);

		GetGenomeResponse GetGenome(GetGenomeRequest request, IRequestOptions options = null);

        ListRunFilesResponse ListRunFiles(ListRunFilesRequest request, IRequestOptions options = null);

		CreateAppResultResponse CreateAppResult(CreateAppResultRequest request, IRequestOptions options = null);

		ListSampleFilesResponse ListSampleFiles(ListSampleFilesRequest request, IRequestOptions options = null);

		ListAppResultFilesResponse ListAppResultFiles(ListAppResultFilesRequest request, IRequestOptions options = null);

		GetFileInformationResponse GetFilesInformation(GetFileInformationRequest request, IRequestOptions options = null);

		GetVariantHeaderResponse GetVariantHeader(GetVariantHeaderRequest request, IRequestOptions options = null);

		ListVariantsResponse ListVariants(ListVariantsRequest request, IRequestOptions options = null);

		GetCoverageResponse GetCoverage(GetCoverageRequest request, IRequestOptions options = null);

		GetCoverageMetadataResponse GetCoverageMetadata(GetCoverageMetadataRequest request, IRequestOptions options = null);

		OAuthDeviceAuthResponse BeginOAuthDeviceAuth(OAuthDeviceAuthRequest request, IRequestOptions options = null);

		OAuthDeviceAccessTokenResponse FinishOAuthDeviceAuth(OAuthDeviceAccessTokenRequest request, IRequestOptions options = null);

		OAuthV2AccessTokenResponse GetOAuthAccessToken(OAuthV2AccessTokenRequest request, IRequestOptions options = null);

		UploadFileToAppResultResponse UploadFileToAppResult(UploadFileToAppResultRequest request, IRequestOptions options = null);

        void DownloadFile(string fileId, Stream stream, CancellationToken token = new CancellationToken());

        void DownloadFile(FileCompact file, Stream stream, CancellationToken token = new CancellationToken());

        void DownloadFile(FileCompact file, string filePath, CancellationToken token = new CancellationToken());

        void DownloadFile(FileCompact file, string filePath, int maxThreadCount, CancellationToken token = new CancellationToken());

        void DownloadFile(FileCompact file, string filePath, int maxChunkSize, int maxThreadCount, CancellationToken token = new CancellationToken());
        
        event FileDownloadProgressChangedEventHandler FileDownloadProgressChanged;

        GetApiMetaResponse GetApiMeta(GetApiMetaRequest request, IRequestOptions options = null);

        /// <summary>
        /// Retrieve list of properties for a resource. Resulting list may require paging.
        /// </summary>
        /// <remarks>
        /// GET: {resource}/properties
        /// </remarks>
        ListPropertiesResponse ListPropertiesForResource(ListPropertiesRequest request, IRequestOptions options = null);

        /// <summary>
        /// Retrieve one property given its parent resource and name
        /// </summary>
        /// <remarks>GET: {resource}/properties/{name}</remarks>
        GetPropertyResponse GetPropertyForResource(GetPropertyRequest request, IRequestOptions options = null);

        /// <summary>
        /// Retrieve list of items for a multi-value property (property having a type ending in '[]'). Resulting list may require paging.
        /// </summary>
        /// <remarks>GET: {resource}/properties/{name}/items</remarks>
        ListPropertyItemsResponse ListPropertyItems(ListPropertyItemsRequest request, IRequestOptions options = null);

        /// <summary>
        /// Add or modify properties associated with a resource. The list of modified properties will be returned.
        /// </summary>
        /// <remarks>POST: {resource}/properties</remarks>
        SetPropertiesResponse SetPropertiesForResource(SetPropertiesRequest request, IRequestOptions options = null);

        /// <summary>
        /// Delete a property given its parent resource and name.
        /// </summary>
        /// <remarks>DELETE: {resource}/properties/{name}</remarks>
        DeletePropertyResponse DeletePropertyForResource(DeletePropertyRequest request, IRequestOptions options = null);

        /// <summary>
        /// Retrieve resources from the API based on a search query
        /// </summary>
        SearchResponse Search(SearchRequest request, IRequestOptions options = null);

        GetPurchaseResponse GetPurchase(GetPurchaseRequest request, IRequestOptions options = null);

        CreatePurchaseResponse CreatePurchase(CreatePurchaseRequest request, IRequestOptions options = null);

        CreatePurchaseRefundResponse CreatePurchaseRefund(CreatePurchaseRefundRequest request, IRequestOptions options = null);

        ListPurchasedProductsResponse ListPurchasedProducts(ListPurchasedProductsRequest request, IRequestOptions options = null);

        ListSampleLibrariesResponse ListSampleLibrariesFromRun(ListSampleLibrariesFromRunRequest request, IRequestOptions options = null);

        /// <summary>
        /// Create a biological sample
        /// </summary>
        /// <remarks>POST: /biologicalsamples</remarks>
        CreateBiologicalSampleResponse CreateBiologicalSample(CreateBiologicalSampleRequest request, IRequestOptions options = null);

        /// <summary>
        /// Create a library container
        /// </summary>
        /// <remarks>POST: /librarycontainers</remarks>
        CreateLibraryContainerResponse CreateLibraryContainer(CreateLibraryContainerRequest request, IRequestOptions options = null);
        /// <summary>
        /// Create or Update the container libraries (mapping)
        /// </summary>
        /// <remarks>POST: /librarycontainers/{Id}/libraries</remarks>
        CreateOrUpdateContainerLibrariesResponse CreateOrUpdateContainerLibraries(CreateOrUpdateContainerLibrariesRequest request, IRequestOptions options = null);
        
        /// <summary>
        /// Create library pool
        /// </summary>
        /// <remarks>POST: /library</remarks>
        CreateLibraryPoolResponse CreateLibraryPool(CreateLibraryPoolRequest request, IRequestOptions options = null);

        /// <summary>
        /// Create planned run
        /// </summary>
        /// <remarks>POST: /plannedruns</remarks>
        CreatePlannedRunResponse CreatePlannedRun(CreatePlannedRunRequest request, IRequestOptions options = null);

        /// <summary>
        /// List supported library prep kits
        /// </summary>
        /// <remarks>GET: /librarypreps</remarks>
        ListSupportedLibraryPrepKitsResponse ListSupportedLibraryPrepKits(ListSupportedLibraryPrepKitsRequest request, IRequestOptions options = null);

        /// <summary>
        /// List library prep kit by Id
        /// </summary>
        /// <remarks>GET: /libraryprepkits/{Id}</remarks>
        GetLibraryPrepKitIdResponse GetLibraryPrepKit(GetLibraryPrepKitIdRequest request, IRequestOptions options = null);

        /// <summary>
        /// Pool libraries
        /// </summary>
        /// <remarks>PUT: /librarypools/{Id}/libraries</remarks>
        UpdatePoolToLibraryMappingResponse UpdatePoolToLibraryMapping(UpdatePoolToLibraryMappingRequest request, IRequestOptions options = null);

        /// <summary>
        /// Get Libraries in Container
        /// </summary>
        /// <remarks>GET: /librarycontainers/{Id}/libraries </remarks>
        GetContainerToLibraryMappingResponse GetContainerToLibraryMapping(GetContainerToLibraryMappingRequest request, IRequestOptions options = null);

        /// <summary>
        /// Mark a planned run as ready to sequence
        /// </summary>
        /// <remarks>PUT: "/plannedruns/{Id}/ready</remarks>
        PlannedRunReadyResponse PlannedRunReadyRequest(PlannedRunReadyRequest request, IRequestOptions options = null);
    }
}
