using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleByStatus
{
    /// <summary>
    /// Validator for GetAllSaleCommand
    /// </summary>
    public class GetAllSalesValidator : AbstractValidator<GetAllSalesCommand>
    {
        /// <summary>
        /// Initializes validation rules for GetAllSaleCommand
        /// </summary>
        public GetAllSalesValidator()
        {
            RuleFor(x => x.Status).NotEmpty();
        }
    }
}
