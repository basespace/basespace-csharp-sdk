namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class PostAppSessionRequest
    {
        public PostAppSessionRequest(string name, string status, string statusSummary = "")
        {
            Id = name;
            Status = status;
            StatusSummary = statusSummary;
        }

        public string Id { get; set; }
        public string Status { get; set; }
        public string StatusSummary { get; set; }
    }
}
