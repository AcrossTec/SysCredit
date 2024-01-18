namespace SysCredit.Api.Middlewares;

using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Writers;

using Swashbuckle.AspNetCore.Swagger;

/// <summary>
/// 
/// </summary>
public class SwaggerMiddleware
{
    private readonly RequestDelegate _Next;

    private readonly SwaggerOptions _Options;

    private readonly TemplateMatcher _RequestMatcher;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Next"></param>
    /// <param name="Options"></param>
    public SwaggerMiddleware(RequestDelegate Next, SwaggerOptions? Options)
    {
        _Next = Next;
        _Options = Options ?? new SwaggerOptions();
        _RequestMatcher = new TemplateMatcher(TemplateParser.Parse(_Options.RouteTemplate), new RouteValueDictionary());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="HttpContext"></param>
    /// <param name="SwaggerProvider"></param>
    /// <returns></returns>
    public async Task Invoke(HttpContext HttpContext, ISwaggerProvider SwaggerProvider)
    {
        if (!RequestingSwaggerDocument(HttpContext.Request, out var DocumentName))
        {
            await _Next(HttpContext);
            return;
        }

        try
        {
            string? BasePath = (HttpContext.Request.PathBase.HasValue ? HttpContext.Request.PathBase.Value : null);
            OpenApiDocument OpenApiDocument = ((SwaggerProvider is not IAsyncSwaggerProvider AsyncSwaggerProvider) ? SwaggerProvider.GetSwagger(DocumentName, null, BasePath) : (await AsyncSwaggerProvider.GetSwaggerAsync(DocumentName, null, BasePath)));
            OpenApiDocument OpenApiDocument2 = OpenApiDocument;

            foreach (Action<OpenApiDocument, HttpRequest> PreSerializeFilter in _Options.PreSerializeFilters)
            {
                PreSerializeFilter(OpenApiDocument2, HttpContext.Request);
            }

            if (Path.GetExtension(HttpContext.Request.Path.Value) == ".yaml")
            {
                await RespondWithSwaggerYaml(HttpContext.Response, OpenApiDocument2);
            }
            else
            {
                await RespondWithSwaggerJson(HttpContext.Response, OpenApiDocument2);
            }
        }
        catch (UnknownSwaggerDocument)
        {
            RespondWithNotFound(HttpContext.Response);
        }
    }

    private bool RequestingSwaggerDocument(HttpRequest Request, out string? DocumentName)
    {
        DocumentName = null;
        if (Request.Method != "GET")
        {
            return false;
        }

        RouteValueDictionary RouteValueDictionary = new RouteValueDictionary();
        if (!_RequestMatcher.TryMatch(Request.Path, RouteValueDictionary) || !RouteValueDictionary.ContainsKey("documentName"))
        {
            return false;
        }

        DocumentName = RouteValueDictionary["documentName"]!.ToString();
        return true;
    }

    private void RespondWithNotFound(HttpResponse Response)
    {
        Response.StatusCode = StatusCodes.Status404NotFound;
    }

    private async Task RespondWithSwaggerJson(HttpResponse Response, OpenApiDocument Swagger)
    {
        Response.StatusCode = StatusCodes.Status200OK;
        Response.ContentType = "application/json;charset=utf-8";
        using StringWriter TextWriter = new StringWriter(CultureInfo.InvariantCulture);
        OpenApiJsonWriter Writer = new OpenApiJsonWriter(TextWriter);

        if (_Options.SerializeAsV2)
        {
            Swagger.SerializeAsV2(Writer);
        }
        else
        {
            Swagger.SerializeAsV3(Writer);
        }

        await Response.WriteAsync(TextWriter.ToString(), new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
    }

    private async Task RespondWithSwaggerYaml(HttpResponse Response, OpenApiDocument Swagger)
    {
        Response.StatusCode = StatusCodes.Status200OK;
        Response.ContentType = "text/yaml;charset=utf-8";
        using StringWriter TextWriter = new StringWriter(CultureInfo.InvariantCulture);
        OpenApiYamlWriter Writer = new OpenApiYamlWriter(TextWriter);

        if (_Options.SerializeAsV2)
        {
            Swagger.SerializeAsV2(Writer);
        }
        else
        {
            Swagger.SerializeAsV3(Writer);
        }

        await Response.WriteAsync(TextWriter.ToString(), new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
    }
}