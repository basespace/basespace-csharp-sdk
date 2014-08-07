using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    internal class FileRequestPost<TResult> : AbstractResourceRequest<TResult>
        where TResult : FileResponse
    {
        public FileRequestPost()
        {
            HttpMethod = HttpMethods.POST;
        }

        public FileUploadStatus? UploadStatus { get; set; }

        protected override string GetUrl()
        {
            return string.Format("{0}/files/{1}", Version, Id);
        }
    }
}

