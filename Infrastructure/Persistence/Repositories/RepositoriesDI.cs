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
            // Repositories
            services.AddScoped<IBasePriceLeasingRepository, BasePriceLeasingRepository>();
            services.AddScoped<IBaseEditPriceLeasingRepository, BasePriceEditLeasingRepository>();
            services.AddScoped<ILeasingCalculationRepository, LeasingCalculationRepository>();
            services.AddScoped<IMileageRepository, MileageRepository>();
            services.AddScoped<IModelBaseDataRepository, ModelBaseDataRepository>();
            services.AddScoped<IProductionCostRepository, ProductionCostRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            // Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Services
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IAuthorizationRepository, AuthorizationRepository>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddHttpContextAccessor();
            services.AddMemoryCache();
        }

    }
}

