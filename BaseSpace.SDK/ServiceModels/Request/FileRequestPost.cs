using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class FileRequestPost
    {
	    public string Id { get; set; }

	    public FileUploadStatus? UploadStatus { get; set; }
    }
}

