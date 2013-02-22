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
			var urlWithParameters = AddDefaultQueryParameters(string.Format("{0}/projects/{1}/samples", Version, Id), Offset,
									 Limit, SortDir);
			if (SortBy.HasValue)
				urlWithParameters = string.Format("{0}&{1}={2}", urlWithParameters, QueryParameters.SortBy, SortBy);

			return urlWithParameters;
		}
	}
}
