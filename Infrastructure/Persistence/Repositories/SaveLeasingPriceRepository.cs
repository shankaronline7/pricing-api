using Application.BasePriceLeasing.Command.SavePriceLeasing;
using DSP.Pricing.Application.Common.Interfaces.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pricing.Application.BasePriceLeasing.Queries.BasePriceLeasing;
using Pricing.Application.Common.Interfaces.Data;
using Pricing.Domain.Entities;
using Pricing.Domain.Entities.FunctionalEntities;
using Pricing.Infrastructure.Persistence;
using Pricing.Infrastructure.Persistence.Repositories;

namespace DSP.Pricing.Infrastructure.Persistence.Repositories
{
    public class SaveLeasingPriceRepository : RepositoryBase<BasePriceLeasingDto>, ISaveLeasingPriceRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">ApplicationDbContext</param>
        public SaveLeasingPriceRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to save the Save/Update the Leasing price
        /// </summary>
        /// <param name="saveLeasingPriceDto">List<SaveLeasingPriceDto></param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Returns 1 when the leasing price details are Saved/Updated</returns>       
        public async Task<List<SaveLeasingPriceDto>> SaveLeasingPrice(List<SaveLeasingPriceDto> saveLeasingPriceDto, CancellationToken cancellationToken)
        {
            foreach (var leasingPrice in saveLeasingPriceDto)
            {
                
               
                    var listDiscounts = DeserializeJson<List<Discount>>(leasingPrice.Discounts);
                    var listMargins = DeserializeJson<List<Margin>>(leasingPrice.Margins);
                    var listLeasingrates = DeserializeJson<List<LeasingRate>>(leasingPrice.Leasingrates);
                    var listLeasingFactors = DeserializeJson<List<LeasingFactor>>(leasingPrice.Leasingfactors);
                    if (listDiscounts.Count > 0 || listMargins.Count > 0 || listLeasingrates.Count > 0 || listLeasingFactors.Count > 0)
                    {
                        // Check if existing or create new LeasingPricingConditions
                        var existingLPC = await GetLeasingPricingConditionsAsync(leasingPrice.Id, cancellationToken);
                        double existingCalculationType = 0;
                        if (existingLPC != null)
                        {
                            existingCalculationType = (double)existingLPC.CalculationTypeValue;
                            UpdateLeasingPricingConditions(existingLPC, leasingPrice);
                            _context.LeasingPricingConditions.Update(existingLPC);
                        }
                        else
                        {
                            existingLPC = CreateLeasingPricingConditions(leasingPrice);
                            await _context.LeasingPricingConditions.AddAsync(existingLPC);
                        }
                        await _context.SaveChangesAsync(cancellationToken);
                        // Update or Create LeasingCalculationResults
                        if (existingCalculationType != leasingPrice.CalculationTarget)
                        {
                            var termId = GetTermId(leasingPrice.Term);
                            var leasingCalculationResults = CreateOrUpdateLeasingCalculationResults(
                                existingLPC.ID, termId, listDiscounts, listMargins, listLeasingrates, listLeasingFactors, leasingPrice);
                            if (leasingCalculationResults.Any())
                            {
                                _context.LeasingCalculationResults.UpdateRange(leasingCalculationResults.Where(lcr => lcr.ID != 0));
                                _context.LeasingCalculationResults.AddRange(leasingCalculationResults.Where(lcr => lcr.ID == 0));
                                await _context.SaveChangesAsync(cancellationToken);
                                leasingPrice.Id = existingLPC.ID;
                            }
                        }
                        else
                        {
                            leasingPrice.Id = null;
                        }
                    }
                
            }
            return saveLeasingPriceDto;
        }
        private T DeserializeJson<T>(string json) where T : class
        {
            return string.IsNullOrWhiteSpace(json) ? null : JsonConvert.DeserializeObject<T>(json);
        }
        private async Task<LeasingPricingConditions> GetLeasingPricingConditionsAsync(long? id, CancellationToken cancellationToken)
        {
            return await _context.LeasingPricingConditions
                .Where(t => t.ID == id)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }
        private void UpdateLeasingPricingConditions(LeasingPricingConditions existingLPC, SaveLeasingPriceDto leasingPrice)
        {
            existingLPC.LPC_ValidFrom = leasingPrice.ValidFrom;
            existingLPC.ApprovalStatus = leasingPrice.Status;
            existingLPC.LastModifiedBy = leasingPrice.LastchangedFrom;
            existingLPC.LastModifiedOn = DateTime.UtcNow;
            existingLPC.CalculationTypeValue = leasingPrice.CalculationTarget;
        }
        private LeasingPricingConditions CreateLeasingPricingConditions(SaveLeasingPriceDto leasingPrice)
        {
            return new LeasingPricingConditions
            {
                LPC_ValidFrom = leasingPrice.ValidFrom,
                ModelBaseDataID = leasingPrice.ModelBaseDataID ?? 0,
                ApprovalStatus = leasingPrice.Status,
                CreatedBy = leasingPrice.CreateFrom,
                CreatedOn = DateTime.UtcNow,
                LastModifiedBy = leasingPrice.LastchangedFrom,
                LastModifiedOn = DateTime.UtcNow,
                CalculationTypeValue = leasingPrice.CalculationTarget
            };
        }
        private long GetTermId(long? termValue)
        {
            return _context.Terms
                .Where(t => t.TermValue == termValue)
                .Select(t => t.Id)
                .FirstOrDefault();
        }
        private List<LeasingCalculationResults> CreateOrUpdateLeasingCalculationResults(
             long lpcId, long termId, List<Discount> discounts, List<Margin> margins,
             List<LeasingRate> leasingRates, List<LeasingFactor> leasingFactors, SaveLeasingPriceDto leasingPrice)
        {
            var leasingCalculationResults = new List<LeasingCalculationResults>();
            for (int i = 0; i < discounts.Count; i++)
            {
                var mileageId = _context.Mileages
                    .Where(m => m.MileageValue == discounts[i].MILEAGE)
                    .Select(m => m.ID)
                    .FirstOrDefault();
                var termMileageID = _context.TermMileages
                    .Where(tm => tm.TermID == termId && tm.MileageID == mileageId)
                    .Select(tm => tm.ID)
                    .FirstOrDefault();
                var lcr = _context.LeasingCalculationResults
                    .Where(l => l.LeasingPricingConditionsID == lpcId && l.TermMileageID == termMileageID)
                    .AsNoTracking()
                    .FirstOrDefault();
                leasingCalculationResults.Add(new LeasingCalculationResults
                {
                    ID = lcr?.ID ?? 0,
                    LeasingPricingConditionsID = lpcId,
                    TermMileageID = termMileageID,
                    LeasingDiscount = discounts[i].DISCOUNT,
                    Margin = margins[i].MARGIN,
                    LeasingRate = leasingRates[i].LEASINGRATE,
                    LeasingFactor = leasingFactors[i].LEASINGFACTOR,
                    CreatedBy = lcr?.CreatedBy ?? leasingPrice.CreateFrom,
                    CreatedOn = lcr?.CreatedOn ?? DateTime.UtcNow,
                    LastModifiedBy = leasingPrice.LastchangedFrom,
                    LastModifiedOn = DateTime.UtcNow,
                    ErrorMessage = leasingPrice.ErrorMessage
                });
            }
            return leasingCalculationResults;
        }

    }
}