namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class GetAppResultRequest
    {
        /// <summary>
        /// Get specific AppResult
        /// </summary>
        /// <param name="id">AppResult Id</param>
        public GetAppResultRequest(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}