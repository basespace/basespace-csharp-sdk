using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class FileRequestPost : AbstractResourceRequest<FileResponse>
    {
	    public FileUploadStatus? UploadStatus { get; set; }

		protected override string GetUrl()
		{
			return string.Format("{0}/files/{1}", Version, Id);
		}
	}
}

