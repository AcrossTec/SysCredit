namespace SysCredit.Api.Extensions;

using log4net;
using log4net.Config;

using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;

using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.Stores;

using SysCredit.Helpers;
using SysCredit.Models;

using System.Reflection;

/// <summary>
/// 
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    ///     Logging in .NET Core and ASP.NET Core
    ///     https://learn.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-7.0
    ///     
    ///     Implement a custom logging provider in .NET
    ///     https://learn.microsoft.com/en-us/dotnet/core/extensions/custom-logging-provider
    ///     
    ///     Structured Logging in ASP.NET Core With log4net
    ///     https://code-maze.com/aspnetcore-structured-logging-log4net/
    /// </remarks>
    /// <param name="Builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddSysCreditLogging(this WebApplicationBuilder Builder)
    {
        Builder.Logging.ClearProviders();
        Builder.Logging.AddConsole();

        Builder.Services.AddHttpLogging(Logging =>
        {
            Logging.LoggingFields = HttpLoggingFields.All;
            Logging.RequestBodyLogLimit = 4096;
            Logging.ResponseBodyLogLimit = 4096;

            Logging.MediaTypeOptions.AddText("application/javascript");
        });

        return Builder;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddSysCreditServices(this WebApplicationBuilder Builder)
    {
        Builder.Services.AddSysCreditEndpoints();
        Builder.Services.AddSysCreditSwaggerGen();
        Builder.Services.AddSysCreditStores();
        Builder.Services.AddSysCreditServices();
        Builder.Services.AddSysCreditOptions();
        return Builder;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Services"></param>
    /// <returns></returns>
    public static IServiceCollection AddSysCreditEndpoints(this IServiceCollection Services)
    {
        Services.AddControllers()
            .AddJsonOptions(static Options => Options.JsonSerializerOptions.PropertyNamingPolicy = JsonDefaultNamingPolicy.DefaultNamingPolicy);

        Services.AddEndpointsApiExplorer();

        Services.AddCors(static Options =>
         {
             Options.AddPolicy(SysCreditConstants.CorsAllowSpecificOrigins, static Policy =>
             {
                 Policy.WithMethods("POST", "PUT", "DELETE", "GET", "PATCH");
                 Policy.AllowAnyOrigin().AllowAnyHeader();
             });
         });

        return Services;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Services"></param>
    /// <returns></returns>
    public static IServiceCollection AddSysCreditSwaggerGen(this IServiceCollection Services)
    {
        Services.AddSwaggerGen();
        return Services;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Services"></param>
    /// <returns></returns>
    public static IServiceCollection AddSysCreditStores(this IServiceCollection Services)
    {
        Services.AddScoped<IStore, Store<Entity>>();
        Services.AddScoped(typeof(IStore<>), typeof(Store<>));
        return Services;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Services"></param>
    /// <returns></returns>
    public static IServiceCollection AddSysCreditServices(this IServiceCollection Services)
    {
        var Types = from Type in typeof(Program).Assembly.GetTypes()
                    let ServiceAttribute = Type.GetCustomAttributes().FirstOrDefault(static Attribute => Attribute.GetType().BaseType == typeof(ServiceAttribute)) as ServiceAttribute
                    where ServiceAttribute is not null
                    select (ServiceAttribute.InterfaceType, Type);

        foreach (var (Interface, Type) in Types)
        {
            Services.AddScoped(Interface, Type);
        }

        return Services;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Services"></param>
    /// <returns></returns>
    public static IServiceCollection AddSysCreditOptions(this IServiceCollection Services)
    {
        Services.AddOptions<SysCreditOptions>()
            .Configure<IConfiguration>(static (Options, Configuration) =>
            {
                Options.ConnectionString = Configuration.GetConnectionString(SysCreditConstants.ConnectionStringKey)!;

                if (string.IsNullOrEmpty(Options.ConnectionString))
                {
                    Options.ConnectionString = Environment.GetEnvironmentVariable(SysCreditConstants.ConnectionStringEnv)!;
                }
            });

        Services.Configure<ApiBehaviorOptions>(static Options => { });
        return Services;
    }
}
