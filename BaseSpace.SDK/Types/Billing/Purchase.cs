using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Illumina.BaseSpace.SDK.Types
{
    /// <summary>
    /// Represents a purchase and contains information that is updated throughout the lifecycle of the purchase from creation to completion to refund.
    /// </summary>
    [DataContract]
    public class Purchase
    {
        /// <summary>
        /// Id of the purchase
        /// </summary>
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        /// May be PENDING, COMPLETED, CANCELLED, ERRORED
        /// </summary>
        [DataMember]
        public string Status { get; set; }

        /// <summary>
        /// May be NOTREFUNDED, INITIATED, COMPLETED
        /// </summary>
        [DataMember]
        public string RefundStatus { get; set; }

        [DataMember]
        public DateTime DateCreated { get; set; }

        [DataMember]
        public DateTime DateUpdated { get; set; }

        /// <summary>
        /// A short human readonly order number thats included in the invoice
        /// </summary>
        [DataMember]
        public string InvoiceNumber { get; set; }

        /// <summary>
        /// Amount of iCredits excluding tax for this purchase
        /// </summary>
        [DataMember]
        public decimal Amount { get; set; }

        /// <summary>
        /// Amount of iCredits for taxes in this purchase
        /// </summary>
        [DataMember]
        public decimal AmountOfTax { get; set; }

        /// <summary>
        /// Amount of iCredits including tax in this purchase
        /// </summary>
        [DataMember]
        public decimal AmountTotal { get; set; }

        /// <summary>
        /// List of products included in the purchase
        /// </summary>
        [DataMember]
        public PurchasedProductCompact[] Products { get; set; }

        /// <summary>
        /// Will be 'product' for all product purchases
        /// </summary>
        [DataMember]
        public string PurchaseType { get; set; }

        /// <summary>
        /// Optional AppSession associated with the purchase if an AppSessionId was provided at purchase creation time
        /// </summary>
        [DataMember]
        public AppSessionBilling AppSession { get; set; }

        /// <summary>
        /// Basic information about the customer user.
        /// </summary>
        [DataMember]
        public User User { get; set; }

        /// <summary>
        /// Basic information about the application that initiated the purchase
        /// </summary>
        [DataMember]
        public ApplicationCompact Application { get; set; }

        /// <summary>
        /// The secret code necessary to trigger a refund directly via the API by the application. This field is only returned during purchase creation time.
        /// </summary>
        [DataMember]
        public string RefundSecret { get; set; }

        /// <summary>
        /// If a refund was triggered this is the date when it completed
        /// </summary>
        [DataMember]
        public DateTime? DateRefunded { get; set; }

        /// <summary>
        /// The user initiating the refund. Will be the app owner or an Illumina Customer Service agent.
        /// </summary>
        [DataMember]
        public UserCompact UserRefundedBy { get; set; }

        /// <summary>
        /// Optional comment that may be included with a refund
        /// </summary>
        [DataMember]
        public string RefundComment { get; set; }

        /// <summary>
        /// The URI to which the user should be taken after the purchase is created in order to complete the purchase
        /// </summary>
        [DataMember]
        public string HrefPurchaseDialog { get; set; }
    }
}
