using AutoMapper;
using MediatR;
using Pricing.Application.Common.Interfaces;
using Pricing.Domain.Entities.FunctionalEntities;
namespace DSP.Pricing.Application.BasePriceLeasing.Queries.EditLeasing
{

    public class GetEditBasePriceLeasingQuery : IRequest<List<EditBasePriceLeasingDto>>
    {
        public long[] ModelBaseDataIDs { get; set; }
        public GetEditBasePriceLeasingQuery()
        {

        }
    }

    public class GetEditBasePriceLeasingQueryHandler : IRequestHandler<GetEditBasePriceLeasingQuery, List<EditBasePriceLeasingDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public GetEditBasePriceLeasingQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<EditBasePriceLeasingDto>> Handle(GetEditBasePriceLeasingQuery request, CancellationToken cancellationToken)
        {
            var statusOrder = new[] { "New", "Active", "InWork", "InApproval", "Approved", "Declined" };
            var parameters = new { lstmodelBaseDataID = request.ModelBaseDataIDs, status_order = statusOrder };
            string functionName = "SELECT * FROM edit_getbaseleasingpricing(@lstmodelBaseDataID, @status_order)";
            var listEditBasePriceLeasing = await _unitOfWork.ExecFunctionWithParmsAsync<BasicPriceLeasing>(functionName, parameters);
            var listEditBasePriceLeasingDto = _mapper.Map<List<EditBasePriceLeasingDto>>(listEditBasePriceLeasing);
            return await _unitOfWork.BaseEditPriceLeasing.GetEditBasePriceLeasing(listEditBasePriceLeasingDto, cancellationToken);
        }


    }
}