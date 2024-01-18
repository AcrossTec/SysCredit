namespace SysCredit.Api.Extensions;

using System;
using System.Linq;

using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Swashbuckle.AspNetCore.Swagger;

/// <summary>
/// 
/// </summary>
public static class SwaggerBuilderExtensions
{
    /// <summary>
    ///     Register the Swagger middleware with provided options
    /// </summary>
    public static IApplicationBuilder UseSwagger(this IApplicationBuilder App, SwaggerOptions Options)
    {
        return App.UseMiddleware<Middlewares.SwaggerMiddleware>(Options);
    }

    /// <summary>
    ///     Register the Swagger middleware with optional setup action for DI-injected options
    /// </summary>
    public static IApplicationBuilder UseSwagger(this IApplicationBuilder App, Action<SwaggerOptions>? SetupAction = null)
    {
        SwaggerOptions Options;
        using (var Scope = App.ApplicationServices.CreateScope())
        {
            Options = Scope.ServiceProvider.GetRequiredService<IOptionsSnapshot<SwaggerOptions>>().Value;
            SetupAction?.Invoke(Options);
        }

        return App.UseSwagger(Options);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Endpoints"></param>
    /// <param name="Pattern"></param>
    /// <param name="SetupAction"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IEndpointConventionBuilder MapSwagger(this IEndpointRouteBuilder Endpoints, string Pattern = "/swagger/{documentName}/swagger.{json|yaml}", Action<SwaggerEndpointOptions>? SetupAction = null)
    {
        if (!RoutePatternFactory.Parse(Pattern).Parameters.Any(x => x.Name == "documentName"))
        {
            throw new ArgumentException("Pattern must contain '{documentName}' parameter", nameof(Pattern));
        }

        Action<SwaggerOptions> EndpointSetupAction = Options =>
        {
            var EndpointOptions = new SwaggerEndpointOptions();

            SetupAction?.Invoke(EndpointOptions);

            Options.RouteTemplate = Pattern;
            Options.SerializeAsV2 = EndpointOptions.SerializeAsV2;
            Options.PreSerializeFilters.AddRange(EndpointOptions.PreSerializeFilters);
        };

        var Pipeline = Endpoints.CreateApplicationBuilder()
            .UseSwagger(EndpointSetupAction)
            .Build();

        return Endpoints.MapGet(Pattern, Pipeline);
    }
}
