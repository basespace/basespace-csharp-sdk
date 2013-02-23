namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class UploadFileToAppResultRequest : FileUploadRequestBase<UploadFileToAppResultResponse>
    {
        public UploadFileToAppResultRequest(string resourceId, string name, string directory = null, string resourceIdentifierInUri = "appresults")
			: base(resourceId, name, directory, resourceIdentifierInUri)
        {
        }

        public UploadFileToAppResultRequest()
			: this(null, null)
        {
        }

        public string ResourceIdentifierInUri;

		protected override string GetUrl()
		{
			return string.Format("{0}/{1}/{2}/files", Version, ResourceIdentifierInUri, Id);
		}
	}
}

