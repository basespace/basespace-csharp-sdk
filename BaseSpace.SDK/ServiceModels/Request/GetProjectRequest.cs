namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class GetProjectRequest : AbstractResourceRequest
    {
        /// <summary>
        /// Get a specific project
        /// </summary>
        /// <param name="id">Project Id</param>
        public GetProjectRequest(string id)
        {
            Id = id;
        }
    }
}
