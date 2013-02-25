using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class ListAppResultFilesRequest : ListFilesRequest<ListAppResultFilesResponse, RunFilesSortByParameters>
    {
        /// <summary>
        /// List files belonging to an AppResult
        /// </summary>
        /// <param name="appResultId">AppResult Id</param>
        public ListAppResultFilesRequest(string appResultId) : base(appResultId)
        {
        }

        protected override string GetUrl()
		{
			return BuildUrl(string.Format("{0}/appresults/{1}/files", Version, Id));
		}
	}
}

