using System;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class FileContentRedirectMetaRequest : AbstractResourceRequest<FileContentRedirectMetaResponse>
    {
        public FileContentRedirectMetaRequest(string fileId, FileContentRedirectType redirectType = FileContentRedirectType.True)
			: base (fileId)
        {
            Redirect = redirectType;
        }

        public FileContentRedirectType Redirect { get; set; }

		protected override string GetUrl()
		{
		  return $"{Version}/files/{Id}/content?redirect={Redirect}";
		}
	}
}