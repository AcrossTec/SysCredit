namespace SysCredit.Api.Extensions;

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
public static class ServiceCollectionExtensions
{
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
                 Policy.WithMethods("PUT", "DELETE", "GET", "PATCH");
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
