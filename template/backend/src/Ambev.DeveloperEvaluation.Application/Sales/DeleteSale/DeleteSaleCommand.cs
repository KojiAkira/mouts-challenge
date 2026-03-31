using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    public class DeleteSaleCommand : IRequest<DeleteSaleResponse>
    {
        /// <summary>
        /// The unique identifier of the sale to Cancel
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Initializes a new instance of CancelSaleCommand
        /// </summary>
        /// <param name="id">The ID of the sale to Cancel</param>
        public DeleteSaleCommand(Guid id)
        {
            Id = id;
        }
    }
}
