namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class GetAppSessionRequest : AbstractResourceRequest
    {
        /// <summary>
        /// Get specific AppSession
        /// </summary>
        /// <param name="id">AppSession Id</param>
        public GetAppSessionRequest(string id)
        {
            Id = id;
        }
    }
}
