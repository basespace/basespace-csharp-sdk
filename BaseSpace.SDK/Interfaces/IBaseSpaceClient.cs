﻿using System.IO;
using System.Net;
using System.Threading;
using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK
{
    public interface IBaseSpaceClient
    {
        IWebProxy WebProxy { get; set; }

        GetUserResponse GetUser(GetUserRequest request, IRequestOptions options = null);

		GetRunResponse GetRun(GetRunRequest request, IRequestOptions options = null);

        ListRunsResponse ListRuns(ListRunsRequest request, IRequestOptions options = null);

		GetProjectResponse GetProject(GetProjectRequest request, IRequestOptions options = null);

		ListProjectsResponse ListProjects(ListProjectsRequest request, IRequestOptions options = null);

		CreateProjectResponse CreateProject(CreateProjectRequest request, IRequestOptions options = null);

		GetAppSessionResponse GetAppSession(GetAppSessionRequest request, IRequestOptions options = null);

        CreateAppSessionLogsResponse CreateAppSessionLogs(CreateAppSessionLogsRequest request, IRequestOptions options = null);

		GetSampleResponse GetSample(GetSampleRequest request, IRequestOptions options = null);

        ListSamplesResponse ListSamples(ListSamplesRequest request, IRequestOptions options = null);

        ListAppResultsResponse ListAppResults(ListAppResultsRequest request, IRequestOptions options = null);

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
    }
}
