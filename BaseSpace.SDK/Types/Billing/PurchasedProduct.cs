using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Illumina.BaseSpace.SDK.Types
{
    /// <summary>
    /// Represents a purchased product
    /// </summary>
    [DataContract]
    public class PurchasedProductCompact
    {
        /// <summary>
        /// The purchased product Id
        /// </summary>
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        /// Name of the product
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Quantity of this product included in a given purchase.
        /// </summary>
        [DataMember]
        public decimal Quantity { get; set; }

        /// <summary>
        /// For persistence products, this is the date after which the product is no longer active.
        /// </summary>
        [DataMember(EmitDefaultValue = true)]
        public DateTime? DateExpiration { get; set; }

        /// <summary>
        /// May be NOPERSISTENCE, ACTIVE, EXPIRED
        /// </summary>
        [DataMember]
        public string PersistenceStatus { get; set; }

        /// <summary>
        /// Price paid for the product in iCredits
        /// </summary>
        [DataMember]
        public decimal Price { get; set; }

        /// <summary>
        /// Optional set of tags associated with the product purchase. These may be set at purchase creation time and may be looked up later.
        /// </summary>
        [DataMember]
        public string[] Tags { get; set; }
    }

    /// <summary>
    /// Represents a purchased product
    /// </summary>
    [DataContract]
    public class PurchasedProduct : PurchasedProductCompact
    {
        /// <summary>
        /// Id of the purchase that includes this product
        /// </summary>
        [DataMember]
        public string PurchaseId { get; set; }

        /// <summary>
        /// Date the purchase was processed
        /// </summary>
        [DataMember]
        public DateTime DatePurchased { get; set; }
    }

    public enum PurchasedProductsSortByParameters
    {
        DateCreated
    };
}
