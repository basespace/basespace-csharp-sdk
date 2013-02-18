namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public abstract class AbstractResourceRequest
    {
		protected AbstractResourceRequest()
		{
		}

		protected AbstractResourceRequest(string id)
		{
			Id = id;
		}

        public string Id { get; set; }
    }
}
