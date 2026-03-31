using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleByStatus
{
    public class GetAllSalesCommand : IRequest<List<GetAllSalesResult>>
    {
        /// <summary>
        /// The Status of the sales to retrieve
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// Initializes a new instance of GetAllSalesCommand
        /// </summary>
        /// <param name="status">The status of the sales to retrieve</param>
        public GetAllSalesCommand(Status status)
        {
            this.Status = status;
        }
    }
}
