using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleByStatus
{
    /// <summary>
    /// Response model for GetAllSale operation
    /// </summary>
    public class GetAllSalesResult
    {
        /// <summary>
        /// The unique identifier of the Sale
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// The sale's date
        /// </summary>
        public DateTimeOffset SaleDate { get; set; }
        /// <summary>
        /// The Customer Unique identifier
        /// </summary>
        public Guid CustomerId { get; set; }
        /// <summary>
        /// The customer's full name
        /// </summary>
        public string CustomerName { get; set; } = string.Empty;
        /// <summary>
        /// The Branch Unique identifier
        /// </summary>
        public Guid BranchId { get; set; }
        /// <summary>
        /// The Branch's full name
        /// </summary>
        public string BranchName { get; set; } = string.Empty;
        /// <summary>
        /// The current status of the sale
        /// </summary>
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// Total Amount of Sale
        /// </summary>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// Items of the Sale
        /// </summary>
        public List<GetSaleItemResult> Items { get; set; } = new();
    }
    public class GetAllSalesItemResult
    {
        /// <summary>
        /// The unique identifier of the Sale Item
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// The Product Unique identifier
        /// </summary>
        public Guid ProductId { get; set; }
        /// <summary>
        /// The Product name
        /// </summary>
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// Quantity of item
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Price of the item
        /// </summary>
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// Discount of item
        /// </summary>
        public decimal Discount { get; set; }
        /// <summary>
        /// Total Amount of item
        /// </summary>
        public decimal TotalAmount { get; set; }
    }
}
