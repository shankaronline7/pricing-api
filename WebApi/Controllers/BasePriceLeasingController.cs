using Application.BasePriceLeasing.Command.SavePriceLeasing;
using Application.DTOs;
using DSP.Pricing.Application.BasePriceLeasing.Command.SavePriceLeasing;
using DSP.Pricing.Application.BasePriceLeasing.Queries.EditLeasing;
using DSP.Pricing.Application.BasePriceLeasing.Queries.LeasingCalculation;
using Microsoft.AspNetCore.Mvc;
using Pricing.Application.BasePriceLeasing.Queries.BasePriceLeasing;
using Pricing.WebApi.Controllers;

namespace WebApi.Controllers
{
    public class BasePriceLeasingController : ApiControllerBase
    {
        /// <summary>
        /// Get List of Base Price Leasing Records
        /// </summary> 
        /// <remarks>
        ///  Get list of  Base Price Leasing records with Active,New In work, In Approval, Approved,Declined status and  this api call not required any input 
        /// </remarks>
        /// <returns> This endpoint returns a list of Active Base Price Leasing Detail</returns>
        /// <response code="200">This endpoint returns List of Base Price Leasing Records</response>
        [HttpGet]
        public async Task<ActionResult<List<BasePriceLeasingDto>>> GetBasePriceLeaseing()
        {
            var result = await Mediator.Send(new GetBasePriceLeasingQuery());
            return result == null ?
                NotFound() :
                Ok(result);
        }
        [HttpPost("SavePriceLeasing")]
        public async Task<ActionResult<int>> SaveLeasingPrice(List<SaveLeasingPriceDto> saveLeasingPriceDto)
        {

            var result = await Mediator.Send(new SaveLeasingPriceCommand { Status = Pricing.Domain.Constants.WorkflowStatus.InWork, saveLeasingPriceDto = saveLeasingPriceDto });
            return result == 0 ?
                 NotFound() :
                 Ok();
        }
        /// <response code="200">This endpoint returns a list of Active/In work/In Approval/Approved/New  Base Price Leasing Detail.</response>
        [HttpPost("EditPriceLeasing")]
        public async Task<ActionResult<List<EditBasePriceLeasingDto>>> GetEditPriceLeaseing([FromBody] long[] ModelBaseDataID)
        {

            var result = await Mediator.Send(new GetEditBasePriceLeasingQuery { ModelBaseDataIDs = ModelBaseDataID });
            return result == null ?
                NotFound() :
                Ok(result);
        }

        [HttpPost("GetLeasingCalculationWithDiscount")]
        public async Task<ActionResult<LeasingCalculationResultDto>>
    GetACalculationLeasingWithDiscount(
    [FromBody] LeasingCalculationDto request)
        {
            var result = await Mediator.Send(
                new GetLeasingCalculationWithDiscountQuery
                {
                    Request = request
                });

            return result == null ? NotFound() : Ok(result);
        }
       


    }

}

