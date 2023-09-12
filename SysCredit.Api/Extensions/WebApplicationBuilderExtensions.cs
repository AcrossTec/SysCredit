namespace SysCredit.Api.Extensions;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using log4net.Config;

using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.Stores;

using SysCredit.Helpers;
using SysCredit.Models;

using System.Reflection;
using System.Text;

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
    ///     
    ///     Custom configuration using Log4NetProviderOptions
    ///     https://github.com/huorswords/Microsoft.Extensions.Logging.Log4Net.AspNetCore/blob/develop/doc/CONFIG.md
    ///
    ///     Apache log4net™ Manual - Configuration
    ///     https://logging.apache.org/log4net/release/manual/configuration.html
    /// </remarks>
    /// <param name="Builder"></param>
    /// <returns></returns>
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
    /// 
    /// </summary>
    /// <param name="Builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddSysCreditServices(this WebApplicationBuilder Builder)
    {
        Builder.Services.AddSwaggerDocumentation();
        Builder.Services.AddAuth(Builder.Configuration);
        Builder.Services.AddSysCreditEndpoints();
        Builder.Services.AddSysCreditSwaggerGen();
        Builder.Services.AddSysCreditStores();
        Builder.Services.AddSysCreditServices();
        Builder.Services.AddSysCreditOptions();
        return Builder;
    }
    /// <summary>
    /// Configura y agrega la autenticación JWT (JSON Web Token) a la colección de servicios.
    /// </summary>
    /// <param name="Services">La colección de servicios de la aplicación.</param>
    /// <param name="Configuration">La configuración de la aplicación, que incluye la información del token JWT.</param>
    /// <returns>La colección de servicios con la autenticación JWT configurada.</returns>
    public static IServiceCollection AddAuth(this IServiceCollection Services, IConfiguration Configuration)
    {
        Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(Options =>
            {
                Options.TokenValidationParameters = new TokenValidationParameters
                {
                    // Valida el emisor (issuer) del token.
                    ValidateIssuer = true,

                    // Valida la audiencia del token.
                    ValidateAudience = true,

                    // Valida la vigencia (lifetime) del token.
                    ValidateLifetime = true,

                    // Valida la firma del token usando una clave secreta.
                    ValidateIssuerSigningKey = true,

                    // Establece el emisor válido para la validación del token desde la configuración.
                    ValidIssuer = Configuration["Token:Issuer"],

                    // Establece la audiencia válida para la validación del token desde la configuración.
                    ValidAudience = Configuration["Token:Issuer"],

                    // Establece la clave de firma del token desde la configuración.
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:Key"]!))
                };
            });

        Services.AddAuthorization();

        return Services;
    }

    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection Services)
    {
        Services.AddEndpointsApiExplorer();

        Services.AddSwaggerGen(Configurarion =>
        {
            var securitySchema = new OpenApiSecurityScheme
            {
                Description = "JWT Auth Bearer Scheme",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };

            Configurarion.AddSecurityDefinition("Bearer", securitySchema);

            var securityRequirement = new OpenApiSecurityRequirement
            {
                {
                    securitySchema, new[] {"Bearer"}
                }
            };

            Configurarion.AddSecurityRequirement(securityRequirement);
        });

        return Services;
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
                    let ServiceAttribute = Type.LookupGenericAttribute(typeof(ServiceAttribute<>))
                    where ServiceAttribute is not null
                    let InterfaceType = ServiceAttribute.GetType().GetProperty(nameof(ServiceAttribute<IServiceCollection>.InterfaceType))!.GetValue(ServiceAttribute).As<Type>()
                    select (InterfaceType, Type);

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
