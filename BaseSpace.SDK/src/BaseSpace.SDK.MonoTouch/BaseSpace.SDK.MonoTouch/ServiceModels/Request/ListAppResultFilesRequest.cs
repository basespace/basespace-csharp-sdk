using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class ListAppResultFilesRequest : AbstractResourceListRequest<RunFilesSortByParameters>
    {
        /// <summary>
        /// List files belonging to an AppResult
        /// </summary>
        /// <param name="appResultId">AppResult Id</param>
        public ListAppResultFilesRequest(string appResultId)
        {
            AppResultId = appResultId;
        }

        public string AppResultId { get; set; }
        public string Extensions { get; set; }
    }
}

