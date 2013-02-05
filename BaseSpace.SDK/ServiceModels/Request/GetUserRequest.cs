namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class GetUserRequest : AbstractResourceRequest
    {
        /// <summary>
        /// Get basic user infomation
        /// </summary>
        /// <param name="id">User Id or null for current user</param>
        public GetUserRequest(string id = null)
        {
            Id = id;
        }
    }
}
