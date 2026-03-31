using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Validator for UpdateSaleCommand that defines validation rules for Sale update command.
    /// </summary>
    public class UpdateSaleValidator : AbstractValidator<UpdateSaleCommand>
    {

        /// <summary>
        /// Initializes a new instance of the UpdateSaleValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - CustomerId: Cannot be set to None
        /// - CustomerName: Cannot be set to None,100 Max characters
        /// - BranchId: Cannot be set to None
        /// - BranchName: Cannot be set to None,100 Max characters
        /// - Items: Cannot be set to None
        /// </remarks>
        public UpdateSaleValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.CustomerName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.BranchId).NotEmpty();
            RuleFor(x => x.BranchName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Items).NotEmpty();
            RuleForEach(x => x.Items).ChildRules(item =>
            {
                item.RuleFor(i => i.ProductId).NotEmpty();
                item.RuleFor(i => i.ProductName).NotEmpty();
                item.RuleFor(i => i.Quantity).GreaterThan(0).LessThanOrEqualTo(20);
                item.RuleFor(i => i.UnitPrice).GreaterThan(0);
            });
        }
    }
}
