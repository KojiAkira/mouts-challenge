using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById
{
    /// <summary>
    /// Validator for GetSaleCommand
    /// </summary>
    public class GetSaleByIdValidator : AbstractValidator<GetSaleCommand>
    {
        /// <summary>
        /// Initializes validation rules for GetSaleCommand
        /// </summary>
        public GetSaleByIdValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
