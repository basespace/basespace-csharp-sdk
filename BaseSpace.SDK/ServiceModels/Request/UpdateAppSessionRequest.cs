﻿namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class UpdateAppSessionRequest : AbstractResourceRequest<UpdateAppSessionResponse>
    {
        /// <summary>
        /// Update AppSession
        /// </summary>
        /// <param name="id">AppSession Id</param>
        /// <param name="status">AppSession Status</param>
        public UpdateAppSessionRequest(string id, string status)
			:base (id)
        {
            Status = status;
			HttpMethod = HttpMethods.POST;
        }

        public string Status { get; set; }

        public string StatusSummary { get; set; }

		protected override string GetUrl()
		{
			return string.Format("{0}/appsessions/{1}", Version, Id);
		}

        internal override string GetInfoLogMessage()
        {
            return string.Format("Updating AppSession status to {0} with status summary {1}", Status, StatusSummary);
        }
	}
}
