using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public UpdateSaleHandler(
            ISaleRepository saleRepository,
            IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);
            if (sale is null)
                throw new KeyNotFoundException($"Sale with ID {command.Id} not found.");

            if (sale.Status != Status.Active)
                throw new InvalidOperationException($"Only active sales can be updated.");

            sale.CustomerName = command.CustomerName;
            sale.BranchName = command.BranchName;
            sale.UpdatedAt = DateTime.UtcNow;

            sale.ClearItems();
            foreach (var item in command.Items)
                sale.AddItem(item.ProductId, item.ProductName, item.Quantity, item.UnitPrice);

            var updated = await _saleRepository.UpdateAsync(sale, cancellationToken);
            return _mapper.Map<UpdateSaleResult>(updated);
        }
    }
}
