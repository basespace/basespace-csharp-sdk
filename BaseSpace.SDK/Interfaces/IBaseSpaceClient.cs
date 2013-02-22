using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK
{
    public interface IBaseSpaceClient
    {
		GetUserResponse GetUser(GetUserRequest request, IRequestOptions options = null);

		GetRunResponse GetRun(GetRunRequest request, IRequestOptions options = null);

		GetProjectResponse GetProject(GetProjectRequest request, IRequestOptions options = null);

		ListProjectsResponse ListProjects(ListProjectsRequest request, IRequestOptions options = null);

		CreateProjectResponse CreateProject(CreateProjectRequest request, IRequestOptions options = null);

		GetAppSessionResponse GetAppSession(GetAppSessionRequest request, IRequestOptions options = null);

		GetSampleResponse GetSample(GetSampleRequest request, IRequestOptions options = null);

		ListSamplesResponse ListSamples(ListSamplesRequest request, IRequestOptions options);

		GetAppResultResponse GetAppResult(GetAppResultRequest request, IRequestOptions options = null);

		ListAppResultsResponse ListAppResults(ListAppResultsRequest request, IRequestOptions options);

		GetGenomeResponse GetGenome(GetGenomeRequest request, IRequestOptions options = null);

		ListGenomeResponse ListGenomes(ListGenomeRequest request, IRequestOptions options);

		UpdateAppSessionResponse UpdateAppSession(UpdateAppSessionRequest request, IRequestOptions options = null);

		CreateAppResultResponse CreateAppResult(CreateAppResultRequest request, IRequestOptions options = null);

		ListRunFilesResponse ListRunFiles(ListRunFilesRequest request, IRequestOptions options);

		ListSampleFilesResponse ListSampleFiles(ListSampleFilesRequest request, IRequestOptions options = null);


		ListAppResultFilesResponse ListAppResultFiles(ListAppResultFilesRequest request, IRequestOptions options = null);

		GetFileInformationResponse GetFilesInformation(GetFileInformationRequest request, IRequestOptions options = null);

		GetVariantHeaderResponse GetVariantHeader(GetVariantHeaderRequest request, IRequestOptions options = null);

		ListVariantsResponse ListVariants(ListVariantsRequest request, IRequestOptions options = null);

		GetCoverageResponse GetCoverage(GetCoverageRequest request, IRequestOptions options = null);

		GetCoverageMetadataResponse GetCoverageMetadata(GetCoverageMetadataRequest request,
														IRequestOptions options = null);


	    TResult Send<TResult>(AbstractRequest<TResult> request, IRequestOptions options = null)
		    where TResult : class;

		OAuthDeviceAuthResponse BeginOAuthDeviceAuth(OAuthDeviceAuthRequest request, IRequestOptions options = null);
		OAuthDeviceAccessTokenResponse FinishOAuthDeviceAuth(OAuthDeviceAccessTokenRequest request, IRequestOptions options = null);
		OAuthV2AccessTokenResponse GetOAuthAccessToken(OAuthV2AccessTokenRequest request, IRequestOptions options = null);


		Types.File UploadFileToAppResult(UploadFileToAppResultRequest request,
												IRequestOptions options = null);

        void SetDefaultRequestOptions(IRequestOptions options = null);

        Task DownloadFileTaskByIdAsync(string fileId, Stream stream, CancellationToken token = new CancellationToken());
        Task DownloadFileTaskAsync(FileCompact file, Stream stream, CancellationToken token = new CancellationToken());
        event FileDownloadProgressChangedEventHandler FileDownloadProgressChanged;

    }
}
