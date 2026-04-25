using Api.Configurations;
using Api.Services;
using Application;
using Core.Ports.Auth;
using Core.Ports.Providers;
using Infrastructure;

namespace Api.Configuration;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAppSettings(configuration);
        services.AddHttpContextAccessor();

        services.AddDistributedMemoryCache();
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromHours(1);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
            options.Cookie.SameSite = SameSiteMode.Lax;
        });

        services.AddScoped<ITokenStore, SessionTokenStore>();
        services.AddScoped<ICalendarTokenAccessor, HttpCalendarTokenAccessor>();
        services.AddInfrastructure(configuration);
        services.AddBusiness();
        services.AddAuthentication();
        services.AddPolicies();

        return services;
    }
}
