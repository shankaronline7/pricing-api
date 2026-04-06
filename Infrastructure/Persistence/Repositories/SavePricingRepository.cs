using Application.BasePriceLeasing.Command.SavePriceLeasing;
using Application.Common.Interfaces.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pricing.Application.BasePriceLeasing.Queries.BasePriceLeasing;
using Pricing.Domain.Entities;
using Pricing.Infrastructure.Persistence;
using Pricing.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class SavePricingRepository : RepositoryBase<BasePriceLeasingDto>, ISavePricingRepository
    {
        private readonly ApplicationDbContext _context;

        public SavePricingRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<List<SavePricingDto>> SavePricing(List<SavePricingDto> savePricingDto, CancellationToken cancellationToken)
        {
            foreach (var pricing in savePricingDto)
            {

                var listDiscounts = DeserializeJson<List<Discount>>(pricing.Discounts);
                var listMargins = DeserializeJson<List<Margin>>(pricing.Margins);
                var listLeasingrates = DeserializeJson<List<LeasingRate>>(pricing.Leasingrates);
                var listLeasingFactors = DeserializeJson<List<LeasingFactor>>(pricing.Leasingfactors);

                if (listDiscounts.Count > 0 || listMargins.Count > 0 || listLeasingrates.Count > 0 || listLeasingFactors.Count > 0)
                {

                    var existingLPC = await GetLeasingPricingConditionsAsync(pricing.Id, cancellationToken);

                    double existingCalculationType = 0;

                    if (existingLPC != null)
                    {
                        existingCalculationType = (double)existingLPC.CalculationTypeValue;

                        UpdateLeasingPricingConditions(existingLPC, pricing);

                        _context.LeasingPricingConditions.Update(existingLPC);
                    }
                    else
                    {
                        existingLPC = CreateLeasingPricingConditions(pricing);

                        await _context.LeasingPricingConditions.AddAsync(existingLPC);
                    }

                    await _context.SaveChangesAsync(cancellationToken);

                    if (existingCalculationType != pricing.CalculationTarget)
                    {
                        var termId = GetTermId(pricing.Term);

                        var leasingCalculationResults = CreateOrUpdateLeasingCalculationResults(
                            existingLPC.ID,
                            termId,
                            listDiscounts,
                            listMargins,
                            listLeasingrates,
                            listLeasingFactors,
                            pricing);

                        if (leasingCalculationResults.Any())
                        {
                            _context.LeasingCalculationResults.UpdateRange(leasingCalculationResults.Where(l => l.ID != 0));

                            _context.LeasingCalculationResults.AddRange(leasingCalculationResults.Where(l => l.ID == 0));

                            await _context.SaveChangesAsync(cancellationToken);

                            pricing.Id = existingLPC.ID;
                        }
                    }
                    else
                    {
                        pricing.Id = null;
                    }
                }
            }

            return savePricingDto;
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

        private void UpdateLeasingPricingConditions(LeasingPricingConditions existingLPC, SavePricingDto pricing)
        {
            existingLPC.LPC_ValidFrom = pricing.ValidFrom;
            existingLPC.ApprovalStatus = pricing.Status;
            existingLPC.UpdatedBy = pricing.LastchangedFrom;
            existingLPC.UpdatedOn = DateTime.UtcNow;
            existingLPC.CalculationTypeValue = pricing.CalculationTarget;
        }

        private LeasingPricingConditions CreateLeasingPricingConditions(SavePricingDto pricing)
        {
            return new LeasingPricingConditions
            {
                LPC_ValidFrom = pricing.ValidFrom,
                ModelBaseDataID = pricing.ModelBaseDataID ?? 0,
                ApprovalStatus = pricing.Status,
                CreatedBy = pricing.CreateFrom,
                CreatedOn = DateTime.UtcNow,
                UpdatedBy = pricing.LastchangedFrom,
                UpdatedOn = DateTime.UtcNow,
                CalculationTypeValue = pricing.CalculationTarget
            };
        }

        private long GetTermId(long? termValue)
        {
            return _context.Terms
                .Where(t => t.TermValue == termValue)
                .Select(t => t.ID)
                .FirstOrDefault();
        }

        private List<LeasingCalculationResults> CreateOrUpdateLeasingCalculationResults(
            long lpcId,
            long termId,
            List<Discount> discounts,
            List<Margin> margins,
            List<LeasingRate> leasingRates,
            List<LeasingFactor> leasingFactors,
            SavePricingDto pricing)
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
                    CreatedBy = lcr?.CreatedBy ?? pricing.CreateFrom,
                    CreatedOn = lcr?.CreatedOn ?? DateTime.UtcNow,
                    UpdatedBy = pricing.LastchangedFrom,
                    UpdatedOn = DateTime.UtcNow,
                    ErrorMessage = pricing.ErrorMessage
                });
            }

            return leasingCalculationResults;
        }
    }
}


