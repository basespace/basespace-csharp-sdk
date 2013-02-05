namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class GetRunRequest : AbstractResourceRequest
    {
        /// <summary>
        /// Get specific run
        /// </summary>
        /// <param name="id">Run Id</param>
        public GetRunRequest(string id)
        {
            Id = id;
        }
    }
}
