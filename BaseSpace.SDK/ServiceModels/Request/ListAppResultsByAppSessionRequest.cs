using System;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class ListAppResultsByAppSessionRequest : AbstractResourceListRequest<ListAppResultsResponse, AppResultSortByParameters>
    {
        /// <summary>
        /// List AppResults within an app session
        /// </summary>
        /// <param name="projectId">App Session Id</param>
        public ListAppResultsByAppSessionRequest(string userId, string appSessionId)
            : base(userId)
        {
            this.AppSessionId = appSessionId;
        }

		protected override string GetUrl()
		{
			return String.Format("{0}/users/{1}/appresults", Version, Id);
		}

        public string AppSessionId { get; set; }
	}
}
