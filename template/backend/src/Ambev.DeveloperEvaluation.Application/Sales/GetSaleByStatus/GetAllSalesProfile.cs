using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleByStatus
{
    /// <summary>
    /// Profile for mapping between Sale entity and GetAllSalesResponse
    /// </summary>
    public class GetAllSalesProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for GetAllSales operation
        /// </summary>
        public GetAllSalesProfile()
        {
            CreateMap<Sale, GetAllSalesResult>();
            CreateMap<SaleItem, GetAllSalesItemResult>();
        }
    }
}
