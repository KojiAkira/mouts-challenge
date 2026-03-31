using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleByStatus
{
    public class GetAllSalesHandler : IRequestHandler<GetAllSalesCommand, List<GetAllSalesResult>>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        public GetAllSalesHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }
        public async Task<List<GetAllSalesResult>> Handle(GetAllSalesCommand request, CancellationToken cancellationToken)
        {
            var validator = new GetAllSalesValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sales = await _saleRepository.GetAllbyStatusAsync(request.Status, cancellationToken);
            return _mapper.Map<List<GetAllSalesResult>>(sales);
        }
    }
}
