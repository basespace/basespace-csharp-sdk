namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class GetRunRequest
    {
        /// <summary>
        /// Get specific run
        /// </summary>
        /// <param name="id">Run Id</param>
        public GetRunRequest(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}
