using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Command for creating a new Sale.
    /// </summary>
    /// <remarks>
    /// This command is used to capture the required data for creating a Sale, 
    /// including SaleId, CustomerId, BranchId, SaleItem. 
    /// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
    /// that returns a <see cref="CreateSaleResult"/>.
    /// 
    /// The data provided in this command is validated using the 
    /// <see cref="CreateSaleValidator"/> which extends 
    /// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
    /// populated and follow the required rules.
    /// </remarks>
    public class CreateSaleCommand : IRequest<CreateSaleResult>
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

        public ValidationResultDetail Validate()
        {
            var validator = new CreateSaleValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(e => (ValidationErrorDetail)e)
            };
        }
    }
    public class CreateSaleItemCommand
    {
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
    }
}
