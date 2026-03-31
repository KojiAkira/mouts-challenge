using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem : BaseEntity
    {
        #region properties
        public Guid SaleId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Discount { get; private set; }
        public decimal TotalAmount { get; private set; }
        public Status Status { get; private set; }

        /// <summary>
        /// Gets the date and time when the user was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets the date and time of the last update to the sale's information.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
        #endregion

        public SaleItem()
        {
            CreatedAt = DateTime.UtcNow;
        }

        #region Methods
        /// <summary>
        /// Calculate Limit of same item in Sale.
        /// </summary>
        internal SaleItem(Guid saleId, Guid productId, string productName, int quantity, decimal unitPrice)
        {
            if (quantity > 20)
                throw new ArgumentException("Não é possível vender mais de 20 itens idênticos.");
            if (quantity <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.");

            Id = Guid.NewGuid();
            SaleId = saleId;
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Status = Status.Active;

            CalculateDiscountsAndTotal();
        }

        /// <summary>
        /// Calculate Discounts And Total of Sale.
        /// </summary>
        private void CalculateDiscountsAndTotal()
        {
            decimal discountPercentage = 0m;

            if (Quantity >= 4 && Quantity < 10)
                discountPercentage = 0.10m; // 10%
            else if (Quantity >= 10 && Quantity <= 20)
                discountPercentage = 0.20m; // 20%

            var rawTotal = Quantity * UnitPrice;
            Discount = rawTotal * discountPercentage;
            TotalAmount = rawTotal - Discount;
        }

        /// <summary>
        /// Activates the user account.
        /// Changes the sale status to Active.
        /// </summary>
        public void Activate()
        {
            Status = Status.Active;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Deactivates the sale account.
        /// Changes the sale status to Inactive.
        /// </summary>
        public void Deactivate()
        {
            Status = Status.Inactive;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Blocks the sale.
        /// Changes the sale status to Blocked.
        /// </summary>
        public void Suspend()
        {
            Status = Status.Suspended;
            UpdatedAt = DateTime.UtcNow;
        }
        #endregion
    }
}
