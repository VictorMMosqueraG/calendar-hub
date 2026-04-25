namespace Api.Configurations;

using Api.Filters;
using Core.Constants;
using Core.Dtos.AppSettingDto;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

public static class SwaggerConfiguration
{
    public static void AddSwaggerConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var swaggerSettings = configuration.GetSection(AppSettingConstant.Swagger).Get<SwaggerSettingDto>()
            ?? throw new InvalidOperationException("Swagger settings not configured.");

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = swaggerSettings.Title,
                Version = swaggerSettings.Version
            });

            options.AddSecurityDefinition(swaggerSettings.SecurityName, new OpenApiSecurityScheme
            {
                Description = swaggerSettings.DescriptionToken,
                Name = swaggerSettings.HeaderName,
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = swaggerSettings.Scheme,
                BearerFormat = swaggerSettings.BearerFormat
            });

            options.OperationFilter<AuthorizeCheckOperationFilter>();

            var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly);
            foreach (var xmlFile in xmlFiles)
                options.IncludeXmlComments(xmlFile);

            options.SchemaFilter<SnakeSchemaFilter>();
            options.OperationFilter<SnakeSchemaFilter>();
            options.OperationFilter<RouteParameterDocumentationFilter>();
        });
    }

    public static void AddSwaggerConfiguration(this IApplicationBuilder app, IOptions<SwaggerSettingDto> swaggerSettingDto)
    {
        var settings = swaggerSettingDto.Value;
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.RoutePrefix = settings.RoutePrefix;
            c.SwaggerEndpoint("/swagger/v1/swagger.json", settings.Title);
            c.DocumentTitle = settings.DocumentTitle;
            c.DocExpansion(DocExpansion.None);
        });
    }
}
