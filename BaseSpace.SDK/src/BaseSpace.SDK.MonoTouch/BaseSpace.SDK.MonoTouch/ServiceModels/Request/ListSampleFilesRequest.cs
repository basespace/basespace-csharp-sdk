using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class ListSampleFilesRequest : AbstractResourceListRequest<RunFilesSortByParameters>
    {
        /// <summary>
        /// List files belonging to a sample
        /// </summary>
        /// <param name="sampleId">Sample Id</param>
        public ListSampleFilesRequest(string sampleId)
        {
            SampleId = sampleId;
        }

        public string SampleId { get; set; }
        public string Extensions { get; set; }
    }
}

