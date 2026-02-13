using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSP.Pricing.Application.Common.Interfaces.Data
{
    public interface ILeasingCalculationRepository
    {
        Task<List<int>> GetMileagesAsync(CancellationToken cancellationToken);
    }
}


