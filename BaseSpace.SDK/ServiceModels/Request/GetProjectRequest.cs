namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class GetProjectRequest : AbstractResourceRequest<GetProjectResponse>
    {
        /// <summary>
        /// Get a specific project
        /// </summary>
        /// <param name="id">Project Id</param>
        public GetProjectRequest(string id)
            : base(id)
        {
        }

        protected override string GetUrl()
        {
            return string.Format("{0}/projects/{1}", Version, Id);
        }
    }
}
