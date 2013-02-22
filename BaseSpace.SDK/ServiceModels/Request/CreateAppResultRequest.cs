namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class CreateAppResultRequest : AbstractRequest<CreateAppResultResponse>
    {
        /// <summary>
        /// Post to create AppResult
        /// </summary>
        /// <param name="projectId">Project Id</param>
        /// <param name="name">AppResult Name</param>
        public CreateAppResultRequest(string projectId, string name)
        {
            ProjectId = projectId;
            Name = name;
        }

        public string ProjectId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string HrefAppSession { get; set; }

        public IContentReference<IAbstractResource>[] References;

		protected override string GetUrl()
		{
			return string.Format("{0}/projects/{1}/appresults", Version, ProjectId);
		}
	}
}