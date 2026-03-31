using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale : BaseEntity
    {
        #region properties
        public DateTimeOffset SaleDate { get; set; }
        public Guid CustomerId { get; private set; }
        public string CustomerName { get; set; }
        public Guid BranchId { get; private set; }
        public string BranchName { get; set; }
        public Status Status { get; private set; }
        public decimal TotalAmount { get; private set; }
        private readonly List<SaleItem> _items = new();
        public IReadOnlyCollection<SaleItem> Items => _items.AsReadOnly();
        /// <summary>
        /// Gets the date and time when the user was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Gets the date and time of the last update to the user's information.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
        #endregion

        #region Methods 
        // Construtor vazio para o EF Core
        public Sale()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public Sale(string saleNumber, Guid customerId, string customerName, Guid branchId, string branchName)
        {
            Id = Guid.NewGuid();
            SaleDate = DateTimeOffset.UtcNow;
            CustomerId = customerId;
            BranchId = branchId;
            Status = Status.Active;
        }
        /// <summary>
        /// Add Item in Sale.
        /// </summary>
        public void AddItem(Guid productId, string productName, int quantity, decimal unitPrice)
        {
            if (Status != Status.Active)
                throw new InvalidOperationException("Não é possível adicionar itens a uma venda cancelada.");

            var item = new SaleItem(Id, productId, productName, quantity, unitPrice);
            _items.Add(item);
            RecalculateTotalAmount();
        }

        /// <summary>
        /// Recalculate Total Amount in Sale.
        /// </summary>
        private void RecalculateTotalAmount()
        {
            TotalAmount = _items.Where(i => i.Status == Status.Active).Sum(i => i.TotalAmount);
        }

        /// <summary>
        /// Clear items in Sale and recalculate Total Amount
        /// </summary>
        public void ClearItems()
        {
            _items.Clear();
            RecalculateTotalAmount();
        }

        /// <summary>
        /// Activates the sale .
        /// Changes the sale status to Active.
        /// </summary>
        public void Activate()
        {
            Status = Status.Active;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Deactivates the sale.
        /// Changes the sale status to Inactive.
        /// </summary>
        public void Deactivate()
        {
            Status = Status.Inactive;
            UpdatedAt = DateTime.UtcNow;
            foreach (var item in _items)
            {
                item.Deactivate();
            }
        }

        /// <summary>
        /// Blocks the sale.
        /// Changes the sale status to Blocked.
        /// </summary>
        public void Suspend()
        {
            Status = Status.Suspended;
            UpdatedAt = DateTime.UtcNow;
            foreach (var item in _items)
            {
                item.Suspend();
            }
        }
        #endregion
    }
}
