using MediatR;
using Pricing.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BasePriceLeasing.Queries.LeasingPricingConditionsAudit
{
    // Query
    public record GetLeasingAuditHistoryQuery(long LeasingConditionId)
      : IRequest<List<LeasingAuditDto>>;
    // Handler
    public class GetLeasingAuditHistoryQueryHandler
    : IRequestHandler<GetLeasingAuditHistoryQuery, List<LeasingAuditDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetLeasingAuditHistoryQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<LeasingAuditDto>> Handle(
            GetLeasingAuditHistoryQuery request,
            CancellationToken cancellationToken)
        {
            return await _unitOfWork.LeasingAudit
                .GetAuditByLeasingIdAsync(request.LeasingConditionId, cancellationToken);
        }
    }
}
