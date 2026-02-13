using Application.Common.Interfaces.Data;
using DSP.Pricing.Application.Common.Interfaces.Data;
using Pricing.Domain.Entities;
using Pricing.Infrastructure.Persistence;
using Pricing.Infrastructure.Persistence.Repositories;

namespace Infrastructure.Persistence.Repositories
{
    public class LeasingCalculationRepository
        : RepositoryBase<LeasingCalculationResults>, ILeasingCalculationRepository
    {
        public LeasingCalculationRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public Task<List<int>> GetMileagesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
