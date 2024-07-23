using Application.Repositories.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
                                                            IConfiguration configuration)
    {
        services.AddDbContext<BaseDbContext>(options =>
                                                 options.UseSqlServer(
                                                     configuration.GetConnectionString("ConnectionString")));

        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        return services;
    }
}
