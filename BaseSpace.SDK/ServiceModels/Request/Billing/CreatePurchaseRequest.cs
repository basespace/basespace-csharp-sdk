using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class CreatePurchaseRequest : AbstractRequest<CreatePurchaseResponse>
    {
        /// <summary>
        /// Create a new purchase
        /// </summary>
        /// <param name="products">List of purchased products each with an Id, quantity, tags</param>
        /// <param name="appSessionId">AppSession Id is known when an application is first started. You may optionally connect the purchase to this AppSession by providing the Id here</param>
        public CreatePurchaseRequest(CreatePurchaseRequestProduct[] products, string appSessionId = null)
        {
            HttpMethod = HttpMethods.POST;
            ApiName = ApiNames.BASESPACE_BILLING;

            Products = products;
            AppSessionId = appSessionId;
        }

        /// <summary>
        /// List of purchased products each with an Id, quantity, tags
        /// </summary>
        public CreatePurchaseRequestProduct[] Products { get; set; }

        /// <summary>
        /// AppSession Id is known when an application is first started. You may optionally connect the purchase to this AppSession by providing the Id here
        /// </summary>
        public string AppSessionId { get; set; }

        protected override string GetUrl()
        {
            return string.Format("{0}/purchases", Version);
        }
    }

    public class CreatePurchaseRequestProduct
    {
        /// <summary>
        /// Each purchased product is described with this object
        /// </summary>
        /// <param name="id"></param>
        /// <param name="quantity"></param>
        /// <param name="tags"></param>
        public CreatePurchaseRequestProduct(string id, decimal quantity, string[] tags = null)
        {
            Id = id;
            Quantity = quantity;
            Tags = tags;
        }

        /// <summary>
        /// Product Id (available in the Developer Portal)
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Quantity of this product being purchased. Persistent products must have a quantity of 1.
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Optional set of tags that will remain associated with the purchased product for future lookup
        /// </summary>
        public string[] Tags { get; set; }
    }
}
