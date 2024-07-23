using Application.Helpers.Security.TokenHelper;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;

namespace Application;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddScoped<IAuthService, AuthManager>();
        services.AddScoped<ITokenHelper, JwtHelper>();
        return services;

    }
}
