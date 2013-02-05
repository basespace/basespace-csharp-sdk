using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class FileRequestPost : AbstractResourceRequest
    {
        public FileUploadStatus? UploadStatus { get; set; }
    }
}

