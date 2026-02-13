using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Pricing.Domain.Constants;
using Pricing.Infrastructure.Persistence;
using System.Reflection;

namespace Pricing.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("PricingDb")
                       .ConfigureWarnings(x =>
                           x.Ignore(InMemoryEventId.TransactionIgnoredWarning)
                       ));
        }
        else
        {
            var connectionString = configuration.GetConnectionString("PricingTool");

            // 🔥 CREATE DATASOURCE BUILDER
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);

            // 🔥 MAP POSTGRES ENUM
            dataSourceBuilder.MapEnum<UserStatus>("public.user_status");

            var dataSource = dataSourceBuilder.Build();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(dataSource, npgsqlOptions =>
                {
                    npgsqlOptions.CommandTimeout(180);
                    npgsqlOptions.EnableRetryOnFailure(0);
                }));
        }

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        RepositoriesDI.RepositoriesDependencyInjections(services);

        return services;
    }
}
