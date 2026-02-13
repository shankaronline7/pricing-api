using DSP.Pricing.Application.BasePriceLeasing.Queries.EditLeasing;
using DSP.Pricing.Application.Common.Interfaces.Data;
using Pricing.Infrastructure.Persistence;
using Pricing.Infrastructure.Persistence.Repositories;

namespace DSP.Pricing.Infrastructure.Persistence.Repositories
{

    public class BasePriceEditLeasingRepository : RepositoryBase<EditBasePriceLeasingDto>, IBaseEditPriceLeasingRepository
    {
        private readonly ApplicationDbContext _context;
        public BasePriceEditLeasingRepository(ApplicationDbContext context)
                  : base(context)
        {
            {
                _context = context;
            }
        }
        public async Task<List<EditBasePriceLeasingDto>> GetEditBasePriceLeasing(List<EditBasePriceLeasingDto> editBasePriceLeasingDto, CancellationToken cancellationToken)
        {
            editBasePriceLeasingDto = editBasePriceLeasingDto.Where(sts => sts.Status != "History").ToList();
            // Using Parallel.ForEach to process the list in parallel
            Parallel.ForEach(editBasePriceLeasingDto, (item) =>
            {
                if (item.Status == "Active" && item.ApprovalStatus == "Approved")
                {
                    item.Status = "Active";
                }
                else if (item.Status == null)
                {
                    item.Status = item.ApprovalStatus;
                }


            });
            return await Task.FromResult(editBasePriceLeasingDto);

        }
    }

}
