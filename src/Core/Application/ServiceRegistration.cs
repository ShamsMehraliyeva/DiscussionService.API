using Application.Utilities.JWT;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Application.BusinessRules;
using Application.CrossCuttingConcers.Logging;
using MediatR;

namespace Application;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        //Busines Rules
        services.AddScoped<AuthBusinessRules>();
        
        //Services
        services.AddScoped<ILoggerService, LoggerService>();
        services.AddScoped<IAuthService, AuthManager>();
        services.AddScoped<ITokenHelper, JwtHelper>();

        return services;

    }
}
