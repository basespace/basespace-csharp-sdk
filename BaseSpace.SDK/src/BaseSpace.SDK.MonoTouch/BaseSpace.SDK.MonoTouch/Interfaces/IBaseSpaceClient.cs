﻿using System;
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

        Task<PostProjectResponse> CreateProjectAsync(PostProjectRequest request, IRequestOptions options = null);
        PostProjectResponse CreateProject(PostProjectRequest request, IRequestOptions options = null);

        Task<GetAppSessionResponse> GetAppSessionAsync(GetAppSessionRequest request, IRequestOptions options = null);
        GetAppSessionResponse GetAppSession(GetAppSessionRequest request, IRequestOptions options = null);

        Task<GetSampleResponse> GetSampleAsync(GetSampleRequest request, IRequestOptions options = null);
        GetSampleResponse GetSample(GetSampleRequest request, IRequestOptions options = null);

        Task<ListSamplesResponse> ListSamplesAsync(ListSamplesRequest request, IRequestOptions options);
        ListSamplesResponse ListSamples(ListSamplesRequest request, IRequestOptions options);

        Task<GetAppResultResponse> GetAppResultAsync(GetAppResultRequest request, IRequestOptions options = null);
        GetAppResultResponse GetAppResult(GetAppResultRequest request, IRequestOptions options = null);

        Task<ListAppResultsResponse> ListAppResultsAsync(ListAppResultsRequest request, IRequestOptions options);
        ListAppResultsResponse ListAppResults(ListAppResultsRequest request, IRequestOptions options);

        Task<GetGenomeResponse> GetGenomeAsync(GetGenomeRequest request, IRequestOptions options = null);
        GetGenomeResponse GetGenome(GetGenomeRequest request, IRequestOptions options = null);

        Task<ListGenomeResponse> ListGenomesAsync(ListGenomeRequest request, IRequestOptions options);
        ListGenomeResponse ListGenomes(ListGenomeRequest request, IRequestOptions options);

        Task<UpdateAppSessionResponse> UpdateAppSessionAsync(UpdateAppSessionRequest request,
                                                                 IRequestOptions options = null);
        UpdateAppSessionResponse UpdateAppSession(UpdateAppSessionRequest request, IRequestOptions options = null);

        Task<PostAppResultResponse> CreateAppResultAsync(PostAppResultRequest request, IRequestOptions options = null);
        PostAppResultResponse CreateAppResult(PostAppResultRequest request, IRequestOptions options = null);

        Task<ListRunFilesResponse> ListRunFilesAsync(ListRunFilesRequest request, IRequestOptions options);
        ListRunFilesResponse ListRunFiles(ListRunFilesRequest request, IRequestOptions options);

        Task<ListSampleFilesResponse> ListSampleFilesAsync(ListSampleFilesRequest request, IRequestOptions options);
        ListSampleFilesResponse ListSampleFiles(ListSampleFilesRequest request, IRequestOptions options);


        Task<ListAppResultFilesResponse> ListAppResultFilesAsync(ListAppResultFilesRequest request,
                                                                 IRequestOptions options);
        ListAppResultFilesResponse ListAppResultFiles(ListAppResultFilesRequest request, IRequestOptions options);

        Task<GetVariantHeaderResponse> GetVariantHeaderAsync(GetVariantHeaderRequest request,
                                                             IRequestOptions options = null);
        GetVariantHeaderResponse GetVariantHeader(GetVariantHeaderRequest request, IRequestOptions options = null);

        Task<ListVariantsResponse> ListVariantsAsync(ListVariantsRequest request, IRequestOptions options = null);
        ListVariantsResponse ListVariants(ListVariantsRequest request, IRequestOptions options = null);

        Task<GetCoverageResponse> GetCoverageAsync(GetCoverageRequest request, IRequestOptions options = null);
        GetCoverageResponse GetCoverage(GetCoverageRequest request, IRequestOptions options = null);

        Task<GetCoverageMetadataResponse> GetCoverageMetadataAsync(GetCoverageMetadataRequest request,
                                                                   IRequestOptions options = null);
        GetCoverageMetadataResponse GetCoverageMetadata(GetCoverageMetadataRequest request,
                                                        IRequestOptions options = null);

        OAuthDeviceAuthResponse BeginOAuthDeviceAuth(OAuthDeviceAuthRequest request, IRequestOptions options = null);
        OAuthDeviceAccessTokenResponse FinishOAuthDeviceAuth(OAuthDeviceAccessTokenRequest request, IRequestOptions options = null);

        void SetDefaultRequestOptions(IRequestOptions options = null);
    }
}
