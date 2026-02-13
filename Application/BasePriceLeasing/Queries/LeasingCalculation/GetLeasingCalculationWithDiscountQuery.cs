using Application.Common.Interfaces.Data;
using Application.DTOs;
using DSP.Pricing.Application.Common;
using MediatR;
using Pricing.Application.Common.Interfaces;
using Pricing.Domain.Entities;
using System.Reflection;

namespace DSP.Pricing.Application.BasePriceLeasing.Queries.LeasingCalculation
{
    public class GetLeasingCalculationWithDiscountQuery : IRequest<LeasingCalculationResultDto>
    {
        /// <summary>
        /// Input class of the query
        /// </summary>
      
        public LeasingCalculationDto Request { get; set; }

    }
    /// <summary>
    /// Query handler 
    /// </summary>

    public class GetLeasingCalculationWithDiscountQueryHandler
    : IRequestHandler<GetLeasingCalculationWithDiscountQuery, LeasingCalculationResultDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetLeasingCalculationWithDiscountQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        
        public async Task<LeasingCalculationResultDto> Handle(GetLeasingCalculationWithDiscountQuery request,
        CancellationToken cancellationToken)
        {
            if (request.Request == null)
                return null;

            var input = request.Request;

            var vehicleModels = await _unitOfWork.ModelBaseData
                .FirstOrDefaultAsync(x => x.ModelCode == input.ModelCode);

            if (vehicleModels == null)
                return null;

            var vehicleCost = await _unitOfWork.ProductionCost
                .FirstOrDefaultAsync(x => x.ModelBaseDataID == vehicleModels.ID);

            if (vehicleCost == null)
                return null;

            var mileageEntities = await _unitOfWork.Mileage.GetAllAsync();
            var mileages = mileageEntities.Select(x => x.MileageValue).ToArray();

            var calculated = LeasingFormulaCalculator.Calculate(
                input,
                mileages,
                vehicleModels,
                (double)vehicleCost.ModelBasePrice
            );

            return calculated;
        }

    }
}

