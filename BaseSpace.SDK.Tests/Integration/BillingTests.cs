using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumina.BaseSpace.SDK.ServiceModels;
using Xunit;

namespace Illumina.BaseSpace.SDK.Tests.Integration
{
    public class BillingTests : BaseIntegrationTest
    {
        [Fact(Skip = "Requires test data to be setup")]
        public void CreatePurchase()
        {
            var purchaseResponse = Client.CreatePurchase(
                new CreatePurchaseRequest(new[]
                                              {
                                                  new CreatePurchaseRequestProduct("2c92c0f93e166759013e17f85fbe2ca7", 1, new string[] {"unittest"})
                                              })).Response;

            Assert.NotNull(purchaseResponse);

            var purchaseId = purchaseResponse.Id;
            Assert.Equal("PENDING", purchaseResponse.Status);

            purchaseResponse = Client.GetPurchase(new GetPurchaseRequest(purchaseId)).Response;
            Assert.NotNull(purchaseResponse);
        }

        [Fact(Skip = "Requires test data to be setup")]
        public void ListProductsAfterPurchase()
        {
            var purchaseListResponse = Client.ListPurchasedProducts(new ListPurchasedProductsRequest(tags: "unittest", persistenceStatus: "NOPERSISTENCE", productIds: "2c92c0f93e166759013e17f85fbe2ca7,dummy")).Response;
            Assert.True(purchaseListResponse.Items.Count() >= 1);
        }

        [Fact(Skip = "Requires test data to be setup")]
        public void RefundPurchase()
        {
            var purchaseResponse = Client.CreatePurchaseRefund(new CreatePurchaseRefundRequest("59c2525446714af1a2206d414b5d818a", "f3d25d701cad43fcb4488e40d4be1117", "unit test refund")).Response;
            Assert.NotNull(purchaseResponse);
            Assert.Equal("COMPLETED", purchaseResponse.RefundStatus);
        }
    }
}
