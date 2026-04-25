namespace Infrastructure;

using Application.Interfaces.Services;
using Application.Interfaces.Wrappers;
using Infrastructure.Services;
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
        services.AddHttpClient<GoogleCalendarWrapper>()
            .AddStandardResilienceHandler();

        services.AddHttpClient<OAuthWrapper>();

        services.AddScoped<ITokenStore, SessionTokenStore>();
        services.AddScoped<ICalendarProvider, GoogleCalendarWrapper>();
        services.AddScoped<IOAuthWrapper, OAuthWrapper>();

        return services;
    }
}
