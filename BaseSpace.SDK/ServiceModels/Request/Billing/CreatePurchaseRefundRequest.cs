namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class CreatePurchaseRefundRequest : AbstractRequest<CreatePurchaseRefundResponse>
    {
        /// <summary>
        /// Issue a refund for a purchase
        /// </summary>
        /// <param name="purchaseId">Id of the purchase to refund</param>
        /// <param name="refundSecret">The secret code necessary to trigger a refund directly via the API by the application. This field is only returned during purchase creation time.</param>
        /// <param name="comment">Optional comment to be associated with the refund</param>
        public CreatePurchaseRefundRequest(string purchaseId, string refundSecret, string comment)
        {
            PurchaseId = purchaseId;
            RefundSecret = refundSecret;
            Comment = comment;
            HttpMethod = HttpMethods.POST;
            ApiName = ApiNames.BASESPACE_BILLING;
        }

        /// <summary>
        /// Id of the purchase to refund
        /// </summary>
        public string PurchaseId { get; set; }

        /// <summary>
        /// The secret code necessary to trigger a refund directly via the API by the application. This field is only returned during purchase creation time.
        /// </summary>
        public string RefundSecret { get; set; }

        /// <summary>
        /// Optional comment to be associated with the refund
        /// </summary>
        public string Comment { get; set; }

        protected override string GetUrl()
        {
            return string.Format("{0}/purchases/{1}/refund", Version, PurchaseId);
        }
    }
}
