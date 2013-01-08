namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class PostAppResultRequest
    {
        /// <summary>
        /// Post to create AppResult
        /// </summary>
        /// <param name="projectId">Project Id</param>
        /// <param name="name">AppResult Name</param>
        public PostAppResultRequest(string projectId, string name)
        {
            ProjectId = projectId;
            Name = name;
        }

        public string ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string HrefAppSession { get; set; }
        public IContentReference<IAbstractResource>[] References;
    }
}