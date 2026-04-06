using Application.BasePriceLeasing.Queries.LeasingPricingConditionsAudit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Data
{
    public interface ILeasingAuditRepository
    {
        Task<List<LeasingAuditDto>> GetAuditByLeasingIdAsync(long id, CancellationToken cancellationToken);
    }
}
