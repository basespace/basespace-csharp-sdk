using System;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class ListProjectsRequest : AbstractResourceListRequest<ListProjectsResponse, ProjectsSortByParameters>
    {
        public string Name { get; set; }

		protected override string GetUrl()
		{
			return BuildUrl(String.Format("{0}/users/current/projects", Version));
		}

		protected override bool HasFilters()
		{
			return base.HasFilters() || (Name != null);
		}

		protected override string BuildUrl(string relativeUrl)
		{
			var url =  base.BuildUrl(relativeUrl);

			return UpdateUrl("Name", Name, url);
		}
	}
}
