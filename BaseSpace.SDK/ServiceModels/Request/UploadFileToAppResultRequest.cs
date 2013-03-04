namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class UploadFileToAppResultRequest : FileUploadRequestBase<UploadFileToAppResultResponse>
    {
        public UploadFileToAppResultRequest(string resourceId, string filePath, string directory = null)
            : base(resourceId, filePath, directory, "appresults")
        {
        }
	}
}

