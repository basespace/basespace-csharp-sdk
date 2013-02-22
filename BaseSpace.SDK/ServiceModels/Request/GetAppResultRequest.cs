namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class GetAppResultRequest : AbstractResourceRequest<GetAppResultResponse>
    {
        /// <summary>
        /// Get specific AppResult
        /// </summary>
        /// <param name="id">AppResult Id</param>
        public GetAppResultRequest(string id)
			: base(id)
        {
        }

		protected override string GetUrl()
		{
			return string.Format("{0}/appresults/{1}", Version, Id);
		}
	}
}