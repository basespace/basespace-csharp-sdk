using System;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class ListAppSessionsRequest : AbstractResourceListRequest<ListAppSessionsResponse, AppSessionSortByParameters>
    {
        /// <summary>
        /// List AppSessions within context of a user
        /// </summary>
        /// <param name="projectId">User Id</param>
        public ListAppSessionsRequest(string userId)
            : base(userId)
        {
        }

		protected override string GetUrl()
		{
			return String.Format("{0}/users/{1}/appsessions", Version, Id);
		}

        public string AppID { get; set; }
        public DateTime DateCreated { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
	}
}
