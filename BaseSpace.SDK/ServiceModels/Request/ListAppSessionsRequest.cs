using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class ListAppSessionsRequest : AbstractResourceListRequest<ListAppSessionsResponse, AppSessionSortByParameters>
    {
        /// <summary>
        /// List AppSessions
        /// </summary>
        public ListAppSessionsRequest()
        {
            
        }

        /// <summary>
        /// Only return AppSessions created by this id of an Application
        /// </summary>
        public string AppId { get; set; }

        protected override string GetUrl()
        {
            return string.Format("{0}/users/current/appsessions", Version);
        }
    }
}
