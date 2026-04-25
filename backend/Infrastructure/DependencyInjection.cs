namespace Infrastructure;

using Core.Ports.Auth;
using Core.Ports.Providers;
using Infrastructure.Auth;
using Infrastructure.Providers;
using Infrastructure.Providers.Google;
using global::Microsoft.Extensions.Configuration;
using global::Microsoft.Extensions.DependencyInjection;
using global::Microsoft.Extensions.Http.Resilience;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHttpClient<GoogleCalendarProvider>()
            .AddStandardResilienceHandler();

        services.AddHttpClient<OAuthService>();

        services.AddScoped<ITokenStore, SessionTokenStore>();
        services.AddScoped<ICalendarTokenAccessor, HttpCalendarTokenAccessor>();
        services.AddScoped<ICalendarProvider, GoogleCalendarProvider>();
        services.AddScoped<IOAuthService, OAuthService>();

        return services;
    }
}
