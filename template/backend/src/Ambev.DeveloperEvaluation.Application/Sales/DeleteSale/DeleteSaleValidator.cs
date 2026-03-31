using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    /// <summary>
    /// Validator for CancelSaleCommand
    /// </summary>
    public class DeleteSaleValidator : AbstractValidator<DeleteSaleCommand>
    {
        /// <summary>
        /// Initializes validation rules for CancelSaleCommand
        /// </summary>
        public DeleteSaleValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
