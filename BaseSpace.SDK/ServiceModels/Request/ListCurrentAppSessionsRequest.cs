// Copyright AB SCIEX 2014. All rights reserved.

using System;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
	public class ListCurrentAppSessionsRequest :
		AbstractAppIdRequest<ListCurrentAppSessionsResponse, ApplicationSortByParameters>
	{
		/// <summary>
		/// List Current AppResults
		/// </summary>
		/// <param name="appId">App Id</param>
		public ListCurrentAppSessionsRequest(string appId)
			: base(appId)
		{
		}

		protected override string GetUrl()
		{
			return String.Format("{0}/users/current/appsessions", Version);
		}
	}
}