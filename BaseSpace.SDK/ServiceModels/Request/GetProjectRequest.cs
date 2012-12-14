namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class GetProjectRequest
    {
        /// <summary>
        /// Get a specific project
        /// </summary>
        /// <param name="id">Project Id</param>
        public GetProjectRequest(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}
