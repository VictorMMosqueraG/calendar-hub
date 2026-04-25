namespace Api;

using Api.Attributes;
using Api.Binders;
using Api.Configuration;
using Api.Configurations;
using Api.Filters;
using Core.Dtos.AppSettingDto;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text.RegularExpressions;

public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDependencyInjection(Configuration);

        ValidatorOptions.Global.PropertyNameResolver = (type, memberInfo, expression) =>
            Regex.Replace(memberInfo!.Name, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();

        services.AddControllers(options =>
        {
            options.ModelBinderProviders.Insert(0, new SnakeModelBinderProvider());
            options.Filters.Add<RouteParameterMappingFilter>();
            options.Filters.Add<ApiExceptionFilterAttribute>();
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
            options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.SnakeCaseLower;
        });

        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
        services.AddDataProtection();
        services.AddCors(o => o.AddPolicy("AllowAll", builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        }));
        services.AddSwaggerConfiguration(Configuration);
        services.AddApiVersioningConfiguration();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<SwaggerSettingDto> swaggerSettings)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
        }

        app.UseCors("AllowAll");
        app.UseRouting();
        app.UseSession();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.AddSwaggerConfiguration(swaggerSettings);
    }
}
