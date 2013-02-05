namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class UploadFileToAppResultRequest : FileUploadRequestBase
    {
        public UploadFileToAppResultRequest(string resourceId, string name, bool multipart,string directory = null, string resourceIdentifierInUri = "appresults")
        {
            Id = resourceId;
            Name = name;
            
            if(directory!=null)
                Directory = directory;
            
            MultiPart = multipart;
            ResourceIdentifierInUri = resourceIdentifierInUri;
        }

        public UploadFileToAppResultRequest()
        {
        }

        public string ResourceIdentifierInUri;
    }
}

