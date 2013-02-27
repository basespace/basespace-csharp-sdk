namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class UploadFileToAppResultRequest : FileUploadRequestBase<UploadFileToAppResultResponse>
    {
        public UploadFileToAppResultRequest(string resourceId, string name, string directory = null)
            : base(resourceId, name, directory, "appresults")
        {
        }
	}
}

