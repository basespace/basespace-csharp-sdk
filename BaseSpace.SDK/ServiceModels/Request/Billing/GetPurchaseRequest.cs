namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class GetPurchaseRequest : AbstractResourceRequest<GetPurchaseResponse>
    {
        /// <summary>
        /// Get a specific purchase
        /// </summary>
        /// <param name="id">Purchase Id</param>
        public GetPurchaseRequest(string id)
			: base(id)
        {
            ApiName = ApiNames.BASESPACE_BILLING;
        }

		protected override string GetUrl()
		{
			return string.Format("{0}/purchases/{1}", Version, Id);
		}
	}
}
