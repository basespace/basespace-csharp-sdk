namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class FileContentRedirectMetaRequest : AbstractResourceRequest
    {
        public FileContentRedirectMetaRequest(string fileId)
        {
            Id = fileId;
            RedirectType = FileContentRedirectType.Meta;
        }

        public FileContentRedirectType RedirectType;
    }
}