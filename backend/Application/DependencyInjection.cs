namespace Application;

using Application.Features.Calendar.GetEvents.Services;
using Application.Features.OAuth.ExchangeToken.Services;
using Application.Features.OAuth.GetAuthUrl.Services;
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

        services.AddScoped<AuthUrlBuilderService>();
        services.AddScoped<TokenExchangeService>();
        services.AddScoped<CalendarRequestService>();
        services.AddScoped<CalendarResponseService>();

        return services;
    }
}
