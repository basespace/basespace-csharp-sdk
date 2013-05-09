namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class GetRunRequest : AbstractResourceRequest<GetRunResponse>
    {
        /// <summary>
        /// Get specific run
        /// </summary>
        /// <param name="id">Run Id</param>
        public GetRunRequest(string id)
			: base(id)
        {
        }

		protected override string GetUrl()
		{
			return string.Format("{0}/runs/{1}", Version, Id);
		}

        internal override string GetLogMessage()
        {
            return "";
        }
	}
}
