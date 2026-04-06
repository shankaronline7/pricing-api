using Application.BasePriceLeasing.Command.SavePriceLeasing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Data
{
    public interface ISavePricingRepository
    {
        Task<List<SavePricingDto>> SavePricing(
            List<SavePricingDto> savePricingDto,
            CancellationToken cancellationToken);
    }
}
