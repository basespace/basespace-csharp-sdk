using System;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class FileContentRedirectMetaRequest : AbstractResourceRequest<FileContentRedirectMetaResponse>
    {
        public FileContentRedirectMetaRequest(string fileId, FileContentRedirectType redirectType = FileContentRedirectType.Meta)
			: base (fileId)
        {
            Redirect = redirectType;
        }

        public FileContentRedirectType Redirect { get; set; }

		protected override string GetUrl()
		{
		    var strRedirect = Redirect == FileContentRedirectType.True ? "?redirect=proxy" : String.Empty;
            return $"{Version}/files/{Id}/content{strRedirect}";
		}
	}
}