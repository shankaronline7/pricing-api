using Application.Common.Interfaces.Data;
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
    
        /* public async Task<List<int>> GetAnnualMileagesAsync()
         {
             return new List<int>
         {
             5000, 10000, 15000, 20000, 25000,
             30000, 35000, 40000, 45000, 50000,
             60000, 70000, 80000, 90000,
             100000, 120000, 140000
         };*/
        public class MileageRepository : RepositoryBase<MileageModel>, IMileageRepository
        {
            public MileageRepository(ApplicationDbContext context)
                : base(context) { }
        }
    }
    


