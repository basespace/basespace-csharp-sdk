using System;
using System.Diagnostics;

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
		    var url = $"{Version}/files/{Id}/content";//?redirect={Redirect}
            Debug.WriteLine(url);
		    return url;
		}
	}
}