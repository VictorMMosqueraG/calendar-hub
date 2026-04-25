namespace Api.Configuration;

using Core.Constants;
using Core.Dtos.AppSettingDto;

public static class AppSettingsConfiguration
{
    public static IServiceCollection AddAppSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SwaggerSettingDto>(o => configuration.GetSection(AppSettingConstant.Swagger).Bind(o));
        services.Configure<CalendarSettingsDto>(o => configuration.GetSection(AppSettingConstant.Calendar).Bind(o));
        services.Configure<OAuthSettingsDto>(o => configuration.GetSection(AppSettingConstant.OAuth).Bind(o));

        return services;
    }
}