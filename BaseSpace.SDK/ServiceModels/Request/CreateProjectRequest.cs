namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class CreateProjectRequest : AbstractRequest<CreateProjectResponse>
    {
        public CreateProjectRequest(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

		protected override string GetUrl()
		{
			return string.Format("{0}/projects", Version);
		}
	}
}
