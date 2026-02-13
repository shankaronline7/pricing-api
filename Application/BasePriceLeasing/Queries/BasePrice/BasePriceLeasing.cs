using AutoMapper;
using Pricing.Domain.Entities.FunctionalEntities;
using MediatR;
using Pricing.Application.Common.Interfaces;
namespace Pricing.Application.BasePriceLeasing.Queries.BasePriceLeasing
{
    public record GetBasePriceLeasingQuery : IRequest<List<BasePriceLeasingDto>>;
    public class GetBasePriceLeasingQueryHandler : IRequestHandler<GetBasePriceLeasingQuery, List<BasePriceLeasingDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public GetBasePriceLeasingQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        // Trail 0
        public async Task<List<BasePriceLeasingDto>> Handle(GetBasePriceLeasingQuery request, CancellationToken cancellationToken)
        {
            var listBasePriceLeasing = await _unitOfWork.ExecFunctionAsync<BasicPriceLeasing>("SELECT * FROM fn_getbasicpriceleasing()");
            var listHistoryBasePriceLeasingDto = _mapper.Map<List<BasePriceLeasingDto>>(listBasePriceLeasing);
            return await _unitOfWork.BasePriceLeasing.GetBasePriceLeasing(listHistoryBasePriceLeasingDto, cancellationToken);
        }

    }


}










