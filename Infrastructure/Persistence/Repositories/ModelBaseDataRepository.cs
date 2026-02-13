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
    public class ModelBaseDataRepository
     : RepositoryBase<ModelBaseData>, IModelBaseDataRepository
    {
        public ModelBaseDataRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        
    }

}
