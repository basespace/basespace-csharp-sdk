namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class GetAppResultRequest : AbstractResourceRequest
    {
        /// <summary>
        /// Get specific AppResult
        /// </summary>
        /// <param name="id">AppResult Id</param>
        public GetAppResultRequest(string id)
        {
            Id = id;
        }
    }
}