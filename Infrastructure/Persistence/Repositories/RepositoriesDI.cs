using Application.Common.Interfaces.Data;
using Application.Interfaces;
using DSP.Pricing.Application.Common.Interfaces.Data;
using DSP.Pricing.Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Pricing.Application.Common.Interfaces;
using Pricing.Application.Common.Interfaces.Data;
using Pricing.Infrastructure.Persistence.Repositories;

namespace Pricing.Infrastructure.Persistence
{
    public static class RepositoriesDI
    {
        public static void RepositoriesDependencyInjections(IServiceCollection services)
        {
            services.AddTransient<IBasePriceLeasingRepository, BasePriceLeasingRepository>();
            services.AddTransient<IBaseEditPriceLeasingRepository, BasePriceEditLeasingRepository>();
            services.AddTransient<ILeasingCalculationRepository, LeasingCalculationRepository>();
            services.AddTransient<IMileageRepository, MileageRepository>();
            services.AddTransient<IModelBaseDataRepository, ModelBaseDataRepository>();
            services.AddTransient<IProductionCostRepository, ProductionCostRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IRoleRepository, RoleRepository>();





        }
    }
}
