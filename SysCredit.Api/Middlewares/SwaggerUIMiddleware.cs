namespace SysCredit.Api.Middlewares;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Swashbuckle.AspNetCore.SwaggerUI;

using SysCredit.Helpers;

/// <summary>
///     Middleware para habilitar la parte visual de Swagger.
/// </summary>
public class SwaggerUIMiddleware
{
    private const string EmbeddedFileNamespace = "Swashbuckle.AspNetCore.SwaggerUI.node_modules.swagger_ui_dist";

    private readonly JsonOptions _JsonOptions;

    private readonly SwaggerUIOptions _Options;

    private readonly StaticFileMiddleware _StaticFileMiddleware;

    private readonly JsonSerializerOptions _JsonSerializerOptions;

    /// <summary>
    ///     Middleware para habilitar la parte gráfica de Swagger.
    /// </summary>
    /// <param name="Next">
    ///     A function that can process an HTTP request.
    /// </param>
    /// <param name="HostingEnv">
    ///     Provides information about the web hosting environment an application is running in.
    /// </param>
    /// <param name="LoggerFactory">
    ///     Represents a type used to configure the logging system and create instances of
    ///     Microsoft.Extensions.Logging.ILogger from the registered Microsoft.Extensions.Logging.ILoggerProviders.
    /// </param>
    /// <param name="Options">
    ///     Swagger UI Options.
    /// </param>
    /// <param name="JsonOptions">
    ///     Options to configure Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonInputFormatter
    ///     and Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonOutputFormatter.
    /// </param>
    public SwaggerUIMiddleware(RequestDelegate Next, IWebHostEnvironment HostingEnv, ILoggerFactory LoggerFactory, SwaggerUIOptions? Options, IOptions<JsonOptions> JsonOptions)
    {
        _Options = Options ?? new SwaggerUIOptions();
        _JsonOptions = JsonOptions.Value;
        _StaticFileMiddleware = CreateStaticFileMiddleware(Next, HostingEnv, LoggerFactory, Options!);
        _JsonSerializerOptions = new JsonSerializerOptions();
        _JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        _JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        _JsonSerializerOptions.TypeInfoResolver = _JsonOptions.JsonSerializerOptions.TypeInfoResolver;
    }

    /// <summary>
    ///     
    /// </summary>
    /// <param name="HttpContext">
    ///     Encapsulates all HTTP-specific information about an individual HTTP request.
    /// </param>
    /// <returns></returns>
    public async Task Invoke(HttpContext HttpContext)
    {
        string Method = HttpContext.Request.Method;
        string Value = HttpContext.Request.Path.Value!;

        if (Method == "GET" && Regex.IsMatch(Value, "^/?" + Regex.Escape(_Options.RoutePrefix) + "/?$", RegexOptions.IgnoreCase))
        {
            string Location = ((string.IsNullOrEmpty(Value) || Value.EndsWith("/")) ? "index.html" : (Value.Split('/').Last() + "/index.html"));
            RespondWithRedirect(HttpContext.Response, Location);
        }
        else if (!(Method == "GET") || !Regex.IsMatch(Value, "^/" + Regex.Escape(_Options.RoutePrefix) + "/?index.html$", RegexOptions.IgnoreCase))
        {
            await _StaticFileMiddleware.Invoke(HttpContext);
        }
        else
        {
            await RespondWithIndexHtml(HttpContext.Response);
        }
    }

    private StaticFileMiddleware CreateStaticFileMiddleware(RequestDelegate Next, IWebHostEnvironment HostingEnv, ILoggerFactory LoggerFactory, SwaggerUIOptions Options)
    {
        StaticFileOptions StaticFileOptions = new()
        {
            RequestPath = (string.IsNullOrEmpty(Options.RoutePrefix) ? string.Empty : ("/" + Options.RoutePrefix)),
            FileProvider = new EmbeddedFileProvider(typeof(Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIMiddleware).GetTypeInfo().Assembly, EmbeddedFileNamespace)
        };

        return new StaticFileMiddleware(Next, HostingEnv, Microsoft.Extensions.Options.Options.Create(StaticFileOptions), LoggerFactory);
    }

    private void RespondWithRedirect(HttpResponse Response, string Location)
    {
        Response.StatusCode = StatusCodes.Status301MovedPermanently;
        Response.Headers["Location"] = Location;
    }

    private async Task RespondWithIndexHtml(HttpResponse Response)
    {
        Response.StatusCode = StatusCodes.Status200OK;
        Response.ContentType = "text/html;charset=utf-8";

        using Stream Stream = _Options.IndexStream();
        using StreamReader Reader = new StreamReader(Stream);
        StringBuilder StringBuilder = new StringBuilder(await Reader.ReadToEndAsync());

        foreach (KeyValuePair<string, string> IndexArgument in GetIndexArguments())
        {
            StringBuilder.Replace(IndexArgument.Key, IndexArgument.Value);
        }

        await Response.WriteAsync(StringBuilder.ToString(), Encoding.UTF8);
    }

    private IDictionary<string, string> GetIndexArguments()
    {
        var ConfigObjectTypeInfo = _JsonSerializerOptions.GetTypeInfo(_Options.ConfigObject.GetType());
        var OAuthConfigObjectTypeInfo = _JsonSerializerOptions.GetTypeInfo(_Options.OAuthConfigObject.GetType());
        var InterceptorsTypeInfo = _JsonSerializerOptions.GetTypeInfo(_Options.Interceptors.GetType());

        return new Dictionary<string, string>
        {
            ["%(DocumentTitle)"] = _Options.DocumentTitle,
            ["%(HeadContent)"] = _Options.HeadContent,
            ["%(ConfigObject)"] = JsonSerializer.Serialize(_Options.ConfigObject, ConfigObjectTypeInfo),
            ["%(OAuthConfigObject)"] = JsonSerializer.Serialize(_Options.OAuthConfigObject, OAuthConfigObjectTypeInfo),
            ["%(Interceptors)"] = JsonSerializer.Serialize(_Options.Interceptors, InterceptorsTypeInfo)
        };
    }
}
