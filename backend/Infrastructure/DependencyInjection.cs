namespace Infrastructure;

using Application.Interfaces.Wrappers;
using Application.Interfaces.Services;
using Core.Ports.Providers;
using Infrastructure.Services;
using Infrastructure.Providers;
using Infrastructure.Providers.Google;
using Infrastructure.Wrappers;
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

        services.AddHttpClient<OAuthWrapper>();

        services.AddScoped<ITokenStore, SessionTokenStore>();
        services.AddScoped<ICalendarTokenAccessor, HttpCalendarTokenAccessor>();
        services.AddScoped<ICalendarProvider, GoogleCalendarProvider>();
        services.AddScoped<IOAuthWrapper, OAuthWrapper>();

        return services;
    }
}
