
namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public abstract class FileUploadRequestBase : AbstractResourceRequest
    {
        public string Name { get; set; }
        public string Directory { get; set; }
        public bool? MultiPart { get; set; }
    }
}
