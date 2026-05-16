namespace Application;

using Application.Features.Calendar.GetEvents.Builders;
using Application.Features.Calendar.GetEvents.Interfaces;
using Application.Features.Calendar.GetEvents.Services;
using Application.Features.OAuth.ExchangeToken.Services;
using Application.Features.OAuth.GetAuthUrl.Builders;
using Application.Features.OAuth.GetAuthUrl.Interfaces;
using Application.Features.OAuth.Interfaces;
using Application.Features.OAuth.Services;
using Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

public static class DependencyInjection
{
    public static IServiceCollection AddBusiness(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => cfg.AddMaps(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        services.AddScoped<IOAuthProviderResolver, OAuthProviderResolver>();
        services.AddScoped<IAuthUrlBuilder, AuthUrlBuilder>();
        services.AddScoped<TokenExchangeService>();
        services.AddScoped<ICalendarRequestBuilder, CalendarRequestService>();
        services.AddScoped<ICalendarResponseMapper, CalendarResponseService>();
        services.AddScoped<ICalendarEventRetriever, CalendarEventRetriever>();
        services.AddScoped<CalendarUrlBuilder>();

        return services;
    }
}
