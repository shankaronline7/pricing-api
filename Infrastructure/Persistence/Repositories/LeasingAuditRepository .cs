using Application.BasePriceLeasing.Queries.LeasingPricingConditionsAudit;
using Application.Common.Interfaces.Data;
using Domain.Entities.FunctionalEntities;
using Microsoft.EntityFrameworkCore;
using Pricing.Application.Common.Interfaces;
using Pricing.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{

   

    public class LeasingAuditRepository : ILeasingAuditRepository
    {
        private readonly ApplicationDbContext _context;

        public LeasingAuditRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<LeasingAuditDto>> GetAuditByLeasingIdAsync(
            long id,
            CancellationToken cancellationToken)
        {
            return await _context.Set<LPC_Audit>()
                .Where(x => x.LPC_Audit_ID == id)
                .OrderByDescending(x => x.updated_date)
                .Select(x => new LeasingAuditDto
                {
                    LpcAuditId = x.LPC_Audit_ID,   // mapped correctly
                    ID = x.ID,
                    ModelBaseDataID = x.ModelBaseDataID,
                    Operation = x.Operation,

                    CalculationType = x.CalculationType,
                    CalculationTypeValue = x.CalculationTypeValue,

                    LPC_ValidFrom = x.LPC_ValidFrom,
                    LPC_ValidTo = x.LPC_ValidTo,

                    created_by = x.created_by,
                    created_date = x.created_date,

                    updated_by = x.updated_by,
                    updated_date = x.updated_date,

                    Status = x.Status,

                    ApprovalID = x.ApprovalID,
                    ApprovedOn = x.ApprovedOn,
                    ApprovalStatus = x.ApprovalStatus,
                    ApprovalRemarks = x.ApprovalRemarks
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}