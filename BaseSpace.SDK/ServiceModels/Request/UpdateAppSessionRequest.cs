namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class UpdateAppSessionRequest
    {
        /// <summary>
        /// Update AppSession
        /// </summary>
        /// <param name="id">AppSession Id</param>
        /// <param name="status">AppSession Status</param>
        public UpdateAppSessionRequest(string id, string status)
        {
            Id = id;
            Status = status;
        }

        public string Id { get; set; }
        public string Status { get; set; }
        public string StatusSummary { get; set; }
    }
}
