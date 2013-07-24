using Illumina.BaseSpace.SDK.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        internal override string GetLogMessage()
        {
            return "ListFileSetFilesRequest";
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
