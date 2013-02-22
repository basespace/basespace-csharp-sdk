using System;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class FileContentRedirectMetaRequest : AbstractResourceRequest<FileContentRedirectMetaResponse>
    {
        public FileContentRedirectMetaRequest(string fileId)
			: base (fileId)
        {
            RedirectType = FileContentRedirectType.Meta;
        }

        public FileContentRedirectType RedirectType;

		protected override string GetUrl()
		{
			return String.Format("{0}/files/{1}/content?Redirect={2}", Version, Id, RedirectType);
		}
	}
}