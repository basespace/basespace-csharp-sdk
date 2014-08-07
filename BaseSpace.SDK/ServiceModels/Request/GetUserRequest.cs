namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class GetUserRequest : AbstractResourceRequest<GetUserResponse>
    {
        /// <summary>
        /// Get basic user infomation
        /// </summary>
        /// <param name="id">User Id or null for current user</param>
        public GetUserRequest(string id = null)
            : base(id)
        {
        }

        protected override string GetUrl()
        {
            return string.Format("{0}/users/{1}", Version, Id ?? "current");
        }
    }
}
