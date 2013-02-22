namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class UploadFileToAppResultRequest : FileUploadRequestBase
    {
        public UploadFileToAppResultRequest(string resourceId, string name,string directory = null, string resourceIdentifierInUri = "appresults")
        {
            Id = resourceId;
            Name = name;
            
            if(directory!=null)
                Directory = directory;
            
            ResourceIdentifierInUri = resourceIdentifierInUri;
        }

        public UploadFileToAppResultRequest()
        {
        }

        public string ResourceIdentifierInUri;
    }
}

