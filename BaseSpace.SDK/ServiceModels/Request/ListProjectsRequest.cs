using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class ListProjectsRequest : AbstractResourceListRequest<ListProjectsResponse, ProjectsSortByParameters>
    {
        public string Name { get; set; }

		protected override string GetUrl()
		{
			var urlWithParameters = AddDefaultQueryParameters(string.Format("{0}/users/current/projects", Version), Offset,
									 Limit, SortDir);
			if (SortBy.HasValue)
			{
				urlWithParameters = string.Format("{0}&{1}={2}", urlWithParameters, QueryParameters.SortBy, SortBy);
			}
			if (!string.IsNullOrEmpty(Name))
			{
				urlWithParameters = string.Format("{0}&{1}={2}", urlWithParameters, QueryParameters.Name, Name);
			}
			return urlWithParameters;
		}
	}
}
