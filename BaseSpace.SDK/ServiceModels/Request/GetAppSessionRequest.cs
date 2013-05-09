using System;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class GetAppSessionRequest : AbstractResourceRequest<GetAppSessionResponse>
    {
        /// <summary>
        /// Get specific AppSession
        /// </summary>
        /// <param name="id">AppSession Id</param>
        public GetAppSessionRequest(string id)
			: base(id)
        {
        }

		protected override string GetUrl()
		{
			return String.Format("{0}/appsessions/{1}", Version, Id);
		}

        internal override string GetLogMessage()
        {
            return "";
        }
	}
}
