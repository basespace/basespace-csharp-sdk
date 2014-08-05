using System;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
	public class ListAppResultsByAppSessionRequest : AbstractResourceListRequest<ListAppResultsResponse, AppResultSortByParameters>

    {
        /// <summary>
        /// List files belonging to an AppResult
        /// </summary>
        /// <param name="appResultId">AppResult Id</param>
		public ListAppResultsByAppSessionRequest(string appResultId)
			: base(appResultId)
        {
        }

        protected override string GetUrl()
		{
			return String.Format("{0}/appsessions/{1}/appresults", Version, Id);
		}
	}
}

