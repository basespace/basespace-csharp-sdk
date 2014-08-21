using System;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class ListAppResultsFromAppSessionRequest : AbstractResourceListRequest<ListAppResultsResponse, AppResultSortByParameters>
    {
        /// <summary>
        /// List AppResults created by an AppSession
        /// </summary>
        /// <param name="appSessionId">appSessionId</param>
        public ListAppResultsFromAppSessionRequest(string appSessionId) 
			: base(appSessionId)
        {
        }

		protected override string GetUrl()
		{
			return String.Format("{0}/appsessions/{1}/appresults", Version, Id);
		}
	}
}
