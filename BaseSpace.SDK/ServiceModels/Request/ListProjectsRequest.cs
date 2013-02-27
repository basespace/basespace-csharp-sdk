using System;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class ListProjectsRequest : AbstractResourceListRequest<ListProjectsResponse, ProjectsSortByParameters>
    {
        public string Name { get; set; }

		protected override string GetUrl()
		{
			return String.Format("{0}/users/current/projects", Version);
		}
	}
}
