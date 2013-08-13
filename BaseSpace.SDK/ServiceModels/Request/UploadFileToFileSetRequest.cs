using Illumina.BaseSpace.SDK.Types;
namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class UploadFileToFileSetRequest : FileUploadRequestBase<UploadFileToFileSetResponse>
    {
        string _uri;
        public UploadFileToFileSetRequest(FileSet fileSet, string filePath, string directory = null)
            : base(fileSet.Id, filePath, directory, string.Empty)
        {
            _uri = fileSet.HrefFiles.ToString();
        }
        protected override string GetUrl()
        {
            return _uri;
        }
	}
}
