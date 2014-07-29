using Illumina.BaseSpace.SDK.Types;
using System;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class ListFileSetFilesRequest : AbstractNestedResourceListRequest<ListFileSetFilesResponse, FileSetFilesSortByParameters>
    {
        Uri _url;
        public ListFileSetFilesRequest(FileSet fs)
        {
            _url = fs.HrefFiles;
        }
        protected override string GetUrl()
        {
            return _url.ToString();
        }

        public string Extensions { get; set; }
    }
    public enum FileSetFilesSortByParameters
    {
        Id,
        Path,
        DateCreated
    }
}
