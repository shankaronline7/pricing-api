using System.Data;
using System.Diagnostics;
using Pricing.Application.BasePriceLeasing.Queries.BasePriceLeasing;
using Pricing.Domain.Entities;
namespace Pricing.Application.Common.Interfaces.Data;

public interface IBasePriceLeasingRepository
{
    /// <summary>
    /// Get list of Base Price Leasing records with Active,New In work, In Approval, Approved,Declined status and this api call not required any input
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<BasePriceLeasingDto>> GetBasePriceLeasing(List<BasePriceLeasingDto> basePriceLeasingDto, CancellationToken cancellationToken);
}
