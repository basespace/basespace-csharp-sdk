using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class UpdateAppSessionRequest : AbstractResourceRequest
    {
        /// <summary>
        /// Update AppSession
        /// </summary>
        /// <param name="id">AppSession Id</param>
        /// <param name="status">AppSession Status</param>
        public UpdateAppSessionRequest(string id, string status, ApplicationCompact application, UserCompact user)
        {
            Id = id;
            Status = status;
            Application = application;
            UserCreatedBy = user;
        }

        public string Status { get; set; }
        public string StatusSummary { get; set; }
        public ApplicationCompact Application { get; set; }
        public UserCompact UserCreatedBy { get; set; }
    }
}
