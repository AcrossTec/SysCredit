namespace SysCredit.Api.Extensions;

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json.Serialization.Metadata;

using Castle.DynamicProxy;

using log4net.Config;

using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.Proxies;
using SysCredit.Api.Stores;
using SysCredit.DataTransferObject;
using SysCredit.Helpers;
using SysCredit.Models;

/// <summary>
///     Métodos de utilería para agregar todas las dependencias necesarias del sistema SysCredit.
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder ConfigureHttpJsonOptions(this WebApplicationBuilder Builder)
    {
        Builder.Services.ConfigureHttpJsonOptions(static Options =>
        {
            Options.SerializerOptions.PropertyNamingPolicy = DefaultJsonNamingPolicy.Default;
            Options.SerializerOptions.TypeInfoResolverChain.Add(HelpersSerializerContext.Default);
            Options.SerializerOptions.TypeInfoResolverChain.Add(SysCreditSerializerContext.Default);
            Options.SerializerOptions.TypeInfoResolverChain.Add(ModelSerializerContext.Default);
            Options.SerializerOptions.TypeInfoResolverChain.Add(DataTransferObjectSerializerContext.Default);
        });

        Builder.Services.Configure<JsonOptions>(static Options =>
        {
            Options.JsonSerializerOptions.WriteIndented = true;
            Options.JsonSerializerOptions.PropertyNamingPolicy = DefaultJsonNamingPolicy.Default;
            Options.JsonSerializerOptions.TypeInfoResolver = JsonTypeInfoResolver.Combine(
                ModelSerializerContext.Default, HelpersSerializerContext.Default,
                SysCreditSerializerContext.Default, DataTransferObjectSerializerContext.Default);
        });

        return Builder;
    }

    /// <summary>
    ///     Configuración del Logger usado por SysCredit.
    /// </summary>
    /// <remarks>
    ///     Logging in .NET Core and ASP.NET Core
    ///     https://learn.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-8.0
    ///     
    ///     Implement a custom logging provider in .NET
    ///     https://learn.microsoft.com/en-us/dotnet/core/extensions/custom-logging-provider
    ///     
    ///     Structured Logging in ASP.NET Core With log4net
    ///     https://code-maze.com/aspnetcore-structured-logging-log4net/
    ///     
    ///     Custom configuration using Log4NetProviderOptions
    ///     https://github.com/huorswords/Microsoft.Extensions.Logging.Log4Net.AspNetCore/blob/develop/doc/CONFIG.md
    ///
    ///     Apache log4net™ Manual - Configuration
    ///     https://logging.apache.org/log4net/release/manual/configuration.html
    /// </remarks>
    /// <param name="Builder">
    ///     Objeto Builder con la información necesario para crear un <see cref="WebApplication" />.
    /// </param>
    /// <returns>
    ///    Regresa el mismo objeto <paramref name="Builder" /> para habilitar la invocación en cadena.
    /// </returns>
    public static WebApplicationBuilder AddSysCreditLogging(this WebApplicationBuilder Builder)
    {
        Builder.Logging.ClearProviders();

        var Configurator = typeof(WebApplicationBuilderExtensions).Assembly.GetCustomAttribute<XmlConfiguratorAttribute>()!;

        Builder.Logging.AddLog4Net(new Log4NetProviderOptions
        {
            Log4NetConfigFileName = Configurator.ConfigFile,
            Watch = Configurator.Watch
        });

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
    ///     Configura todas las injecciones de dependencias de SysCredit.
    /// </summary>
    /// <param name="Builder">
    ///     Objeto Builder con la información necesario para crear un <see cref="WebApplication" />.
    /// </param>
    /// <returns>
    ///    Regresa el mismo objeto <paramref name="Builder" /> para habilitar la invocación en cadena.
    /// </returns>
    [UnconditionalSuppressMessage("Trimming", "IL2026:Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code", Justification = "<Pending>")]
    [UnconditionalSuppressMessage("AOT", "IL3050:Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.", Justification = "<Pending>")]
    public static WebApplicationBuilder AddSysCreditServices(this WebApplicationBuilder Builder)
    {
        Builder.Services.AddSysCreditEndpoints();
        Builder.Services.AddSysCreditSwaggerGen();
        Builder.Services.AddSysCreditStores();
        // TODO: Dar Soporte para AOT Builder.Services.AddSysCreditServices();
        Builder.Services.AddSysCreditOptions();
        return Builder;
    }

    /// <summary>
    ///     Configura la autenticación usado por SysCredit.
    /// </summary>
    /// <param name="Builder">
    ///     Objeto Builder con la información necesario para crear un <see cref="WebApplication" />.
    /// </param>
    /// <returns>
    ///    Regresa el mismo objeto <paramref name="Builder" /> para habilitar la invocación en cadena.
    /// </returns>
    public static WebApplicationBuilder AddSysCreditAuthentication(this WebApplicationBuilder Builder)
    {
        using var ServiceProvider = Builder.Services.BuildServiceProvider();
        var SysCreditOptions = ServiceProvider.GetRequiredService<IOptions<SysCreditOptions>>().Value;
        Builder.Services.AddAuthorization();
        return Builder;
    }

    /// <summary>
    ///     Agrega todos los controladores y sus configuraciones en <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="Services">
    ///     Objeto contenedor IoC de todas las dependencias usadas por SysCredit.
    /// </param>
    /// <returns>
    ///     Regresa el objeto contenedor IoC para habilitar las llamada en cadenas.
    /// </returns>
    public static IServiceCollection AddSysCreditEndpoints(this IServiceCollection Services)
    {
        Services.AddEndpointsApiExplorer();

        Services.AddCors(static Options => Options.AddPolicy(SysCreditConstants.CorsAllowSpecificOrigins, static Policy =>
        {
            Policy.WithMethods(HttpMethods.Get, HttpMethods.Post, HttpMethods.Put, HttpMethods.Patch, HttpMethods.Delete);
            Policy.AllowAnyOrigin().AllowAnyHeader();
        }));

        return Services;
    }

    /// <summary>
    ///     Configura la Autenticación para Swagger.
    /// </summary>
    /// <param name="Services">
    ///     Objeto contenedor IoC de todas las dependencias usadas por SysCredit.
    /// </param>
    /// <returns>
    ///     Regresa el objeto contenedor IoC para habilitar las llamada en cadenas.
    /// </returns>
    public static IServiceCollection AddSysCreditSwaggerGen(this IServiceCollection Services)
    {
        Services.AddSwaggerGen();
        return Services;
    }

    /// <summary>
    ///     Agrega el tipo compuesto <see cref="IStore{TModel}" /> e <see cref="IStore" /> como parte de las dependencias de SysCredit.
    /// </summary>
    /// <param name="Services">
    ///     Objeto contenedor IoC de todas las dependencias usadas por SysCredit.
    /// </param>
    /// <returns>
    ///     Regresa el objeto contenedor IoC para habilitar las llamada en cadenas.
    /// </returns>
    public static IServiceCollection AddSysCreditStores(this IServiceCollection Services)
    {
        Services.AddScoped<IStore, Store<Entity>>();
        Services.AddScoped(typeof(IStore<>), typeof(Store<>));
        return Services;
    }

    /// <summary>
    ///     Agrega todos los Servicos usados por SysCredit al objeto <paramref name="Services" />.
    /// </summary>
    /// <param name="Services">
    ///     Objeto contenedor IoC de todas las dependencias usadas por SysCredit.
    /// </param>
    /// <returns>
    ///     Regresa el objeto contenedor IoC para habilitar las llamada en cadenas.
    /// </returns>
    [RequiresDynamicCode("Invocación de código dinámico para registrar los Servicios y el Proxy.")]
    [RequiresUnreferencedCode("Invocación de código dinámico para registrar los Servicios y el Proxy.")]
    public static IServiceCollection AddSysCreditServices(this IServiceCollection Services)
    {
        var Types = from Type in typeof(Program).Assembly.GetTypes()
                    let ServiceAttribute = Type.LookupGenericAttribute(typeof(ServiceAttribute<>))
                    where ServiceAttribute is not null
                    let InterfaceType = ServiceAttribute.GetType().GetProperty(nameof(ServiceAttribute<IServiceCollection>.InterfaceType))!.GetValue(ServiceAttribute).As<Type>()
                    select (InterfaceType, Type);

        foreach (var (Interface, Type) in Types)
        {
            Services.AddScoped(Interface, Provider => LoggingAdviceServiceInterceptor.Create(Interface, Type, Provider));
        }

        Services.TryAddSingleton<ProxyGenerator>();
        return Services;
    }

    /// <summary>
    ///     Configura mediante Option Pattern las configuraciones generales de SysCredit.
    /// </summary>
    /// <param name="Services">
    ///     Objeto contenedor IoC de todas las dependencias usadas por SysCredit.
    /// </param>
    /// <returns>
    ///     Regresa el objeto contenedor IoC para habilitar las llamada en cadenas.
    /// </returns>
    public static IServiceCollection AddSysCreditOptions(this IServiceCollection Services)
    {
        Services.AddOptions<SysCreditOptions>()
            .Configure<IConfiguration>(static (Options, Configuration) =>
            {
                Configuration.GetSection(SettingsOptions.SysCredit).Bind(Options);

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
