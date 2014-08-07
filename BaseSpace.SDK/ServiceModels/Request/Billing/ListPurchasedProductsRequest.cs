using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class ListPurchasedProductsRequest : AbstractResourceListRequest<ListPurchasedProductsResponse, PurchasedProductsSortByParameters>
    {
        /// <summary>
        /// List the previously purchased products for the user
        /// </summary>
        /// <param name="persistenceStatus">Only return products with a matching PersistenceStatus. Possible values include: ACTIVE, EXPIRED, NOPERSISTENCE</param>
        /// <param name="tags">Filter by comma-separated tags</param>
        /// <param name="productIds">Filter by comma-separated products ids</param>
        public ListPurchasedProductsRequest(string persistenceStatus = null, string tags = null, string productIds = null)
        {
            ApiName = ApiNames.BASESPACE_BILLING;
            PersistenceStatus = persistenceStatus;
            Tags = tags;
            ProductIds = productIds;
        }

        /// <summary>
        /// Only return products with a matching PersistenceStatus. Possible values include: ACTIVE, EXPIRED, NOPERSISTENCE
        /// </summary>
        public string PersistenceStatus { get; set; }

        /// <summary>
        /// Match comma separated tags
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// Match comma separated products ids
        /// </summary>
        public string ProductIds { get; set; }

        protected override string GetUrl()
        {
            return String.Format("{0}/users/current/products", Version);
        }
    }
}
