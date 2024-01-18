namespace SysCredit.Api.Extensions;

using System;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Swashbuckle.AspNetCore.SwaggerUI;

/// <summary>
///     Métodos para configuración de Swagger.
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Builder.SwaggerUIBuilderExtensions" />
public static class SwaggerUIBuilderExtensions
{
    /// <summary>
    ///     Register the SwaggerUI middleware with provided options
    /// </summary>
    public static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder App, SwaggerUIOptions Options)
    {
        return App.UseMiddleware<Middlewares.SwaggerUIMiddleware>(Options);
    }

    /// <summary>
    ///     Register the SwaggerUI middleware with optional setup action for DI-injected options
    /// </summary>
    public static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder App, Action<SwaggerUIOptions>? SetupAction = null)
    {
        SwaggerUIOptions Options;

        using (var Scope = App.ApplicationServices.CreateScope())
        {
            Options = Scope.ServiceProvider.GetRequiredService<IOptionsSnapshot<SwaggerUIOptions>>().Value;
            SetupAction?.Invoke(Options);
        }

        // To simplify the common case, use a default that will work with the SwaggerMiddleware defaults
        if (Options.ConfigObject.Urls == null)
        {
            var HostingEnv = App.ApplicationServices.GetRequiredService<IWebHostEnvironment>();
            Options.ConfigObject.Urls = new[] { new UrlDescriptor { Name = $"{HostingEnv.ApplicationName} v1", Url = "v1/swagger.json" } };
        }

        return App.UseSwaggerUI(Options);
    }
}
