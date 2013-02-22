
namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public abstract class FileUploadRequestBase<TResult> : AbstractResourceRequest<TResult>
		where TResult : class 
    {
        public string Name { get; set; }
        public string Directory { get; set; }
        public bool? MultiPart { get; set; }
    }
}
