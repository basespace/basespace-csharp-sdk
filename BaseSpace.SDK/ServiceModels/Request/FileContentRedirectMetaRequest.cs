using System;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class FileContentRedirectMetaRequest : AbstractResourceRequest<FileContentRedirectMetaResponse>
    {
        public FileContentRedirectMetaRequest(string fileId)
            : base (fileId)
        {
            Redirect = FileContentRedirectType.Meta;
        }

        public FileContentRedirectType Redirect { get; set; }

        protected override string GetUrl()
        {
            return String.Format("{0}/files/{1}/content", Version, Id);
        }
    }
}