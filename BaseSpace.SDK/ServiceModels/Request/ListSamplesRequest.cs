using System;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class ListSamplesRequest : AbstractResourceListRequest<ListSamplesResponse, SamplesSortByParameters>
    {
        /// <summary>
        /// List samples within a project
        /// </summary>
        /// <param name="projectId">Project Id</param>
        public ListSamplesRequest(string projectId)
            : base(projectId)
        {
        }

        protected override string GetUrl()
        {
            return String.Format("{0}/projects/{1}/samples", Version, Id);
        }
    }
}
