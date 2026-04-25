using Api.Configurations;
using Application;
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
            options.Cookie.SameSite = SameSiteMode.None;
            options.Cookie.SecurePolicy = CookieSecurePolicy.None;
        });

        services.AddInfrastructure(configuration);
        services.AddBusiness();
        services.AddAuthentication();
        services.AddPolicies();

        return services;
    }
}
