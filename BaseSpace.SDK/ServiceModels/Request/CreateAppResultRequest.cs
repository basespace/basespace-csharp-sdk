using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    [DataContract]
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
			HttpMethod = HttpMethods.POST;
        }

        [DataMember]
        public string ProjectId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string HrefAppSession { get; set; }

        [DataMember]
        public IContentReference<IAbstractResource>[] References { get; set; }

		protected override string GetUrl()
		{
			return string.Format("{0}/projects/{1}/appresults", Version, ProjectId);
		}
	}
}