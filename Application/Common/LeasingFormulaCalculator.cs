using Application.Common.Models;
using Application.DTOs;
using Pricing.Domain.Entities;
using System.Text.Json;

namespace DSP.Pricing.Application.Common

{

    public static class LeasingFormulaCalculator

    {

        public static LeasingCalculationResultDto Calculate(

            LeasingCalculationDto input,

            int?[] mileages, ModelBaseData model, double vehicleCost)

        {

            var discounts = new List<DiscountValueModel>();

            var margins = new List<MarginValue>();

            var leasingRates = new List<LeasingrateValueModel>();

            var leasingFactors = new List<LeasingfactorValueModel>();

            double baseVehiclePrice = vehicleCost;

            double discountPercent = (double)input.Discount;

            double baseLeasingRate = baseVehiclePrice / input.Term;

            foreach (var mileage in mileages)

            {

                // Step 1: Mileage Factor

                double mf = (double)(mileage / 100000);

                // Step 2: Base Discount

                double bd = baseVehiclePrice * (discountPercent / 100);

                // Step 3: Mileage Adjusted Discount

                double mda = bd * (mf * 0.02);

                double finalDiscount = bd + mda;

                // Step 4: Margin

                double margin = finalDiscount + (mf * 180);

                // Step 5: Leasing Rate

                double leasingRate =

                    baseLeasingRate

                    - (finalDiscount * 0.15)

                    + (mf * 2400);

                // Step 6: Leasing Factor

                double leasingFactor =

                    (leasingRate / baseVehiclePrice) * 100;

                discounts.Add(new DiscountValueModel

                {

                    MILEAGE = (int)mileage,

                    DISCOUNT =

                        Math.Round(finalDiscount, 2).ToString()

                });

                margins.Add(new MarginValue

                {

                    MILEAGE = (int)mileage,

                    MARGIN =

                        Math.Round(margin, 2).ToString()

                });

                leasingRates.Add(new LeasingrateValueModel

                {

                    MILEAGE = (int)mileage,

                    LEASINGRATE = 

                        Math.Round(leasingRate, 2).ToString()

                });

                leasingFactors.Add(new LeasingfactorValueModel

                {

                    MILEAGE = (int)mileage,

                    LEASINGFACTOR =

                        Math.Round(leasingFactor, 2).ToString()

                });

            }

            return new LeasingCalculationResultDto

            {

                Id = 0,

                ModelBaseDataId = model.ID,

                Brand = input.Brand,

                Series = "",

                ModelRange = "",

                ModelDescription = "",

                ModelCode = input.ModelCode,

                CalculationTypeValue = discountPercent.ToString(),

                Term = 12,

                ValidFrom = input.ValidFrom.ToString(),

                Discounts = JsonSerializer.Serialize(discounts),

                Margins = JsonSerializer.Serialize(margins),

                Leasingrates = JsonSerializer.Serialize(leasingRates),

                LeasingFactor = JsonSerializer.Serialize(leasingFactors)

            };

        }

        
    }

}

