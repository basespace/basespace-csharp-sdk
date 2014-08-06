using System;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class ListSampleFilesRequest : ListFilesRequest<ListSampleFilesResponse, RunFilesSortByParameters>
    {
        /// <summary>
        /// List files belonging to a sample
        /// </summary>
        /// <param name="sampleId">Sample Id</param>
        public ListSampleFilesRequest(string sampleId) : base(sampleId)
        {
        }

        protected override string GetUrl()
        {
            return String.Format("{0}/samples/{1}/files", Version, Id);
        }
    }
}

