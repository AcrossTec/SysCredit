namespace SysCredit.Api.Middlewares;

using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.Exceptions;
using SysCredit.Api.Extensions;
using SysCredit.Helpers;

using static Constants.ErrorCodePrefix;
using static Constants.ErrorCodes;

/// <summary>
///     Middleware que captura todas las excepciones no controladas de algún Request.
/// </summary>
/// <param name="Next">
///     Ejecuta el request actual.
/// </param>
/// <param name="Logger">
///     Objeto de Logs para informar sobre el error que se está capturando.
/// </param>
/// <param name="JsonOptions">
///     Opciones para la serialización y deserialización de objectos a JSON.
/// </param>
[ErrorCategory(nameof(SysCreditMiddleware))]
[ErrorCodePrefix(InternalServerErrorPrefix)]
public class SysCreditMiddleware(RequestDelegate Next, ILogger<SysCreditMiddleware> Logger, IOptions<JsonOptions> JsonOptions)
{
    /// <summary>
    ///     Código único del método que captura el error.
    /// </summary>
    public const string SysCreditMiddlewareMethodId = "73E66405-D1D0-44D0-8EAB-9AC7D08742A9";

    /// <summary>
    ///     Método que tiene información del Request actual en ejecución.
    /// </summary>
    /// <param name="Context">
    ///     Información completa del Request que se está ejecutando.
    /// </param>
    /// <returns>
    ///     Task que represanta ha <see cref="InvokeAsync(HttpContext)" /> como una operación asincrona.
    /// </returns>
    [MethodId(SysCreditMiddlewareMethodId)]
    public async Task InvokeAsync(HttpContext Context)
    {
        try
        {
            Context.Request.EnableBuffering();
            await Next(Context);
        }
        catch (ServiceException Exception)
        {
            int StatusCode = Exception.Data[SysCreditConstants.IsFromValidationExceptionKey].As<bool>()
                ? StatusCodes.Status400BadRequest
                : StatusCodes.Status500InternalServerError;

            IResponse Response = await CreateHttpContextResponseDataAsync(Context, StatusCode).ToResponseAsync(Exception.ErrorStatus);
            await Context.Response.WriteAsync(WriteLogErrorResponse(Response));
        }
        catch (StoreException Exception)
        {
            IResponse Response = await CreateHttpContextResponseDataAsync(Context).ToResponseAsync(Exception.ErrorStatus);
            await Context.Response.WriteAsync(WriteLogErrorResponse(Response));
        }
        catch (SysCreditException Exception)
        {
            IResponse Response = await CreateHttpContextResponseDataAsync(Context).ToResponseAsync(Exception.ErrorStatus);
            await Context.Response.WriteAsync(WriteLogErrorResponse(Response));
        }
        catch (Exception Exception)
        {
            IResponse Response = await CreateHttpContextResponseDataAsync(Context).ToResponseAsync(CreateErrorStatusFromException(Exception));
            await Context.Response.WriteAsync(WriteLogErrorResponse(Response));
        }
    }

    /// <summary>
    ///     Establece las configuraciones del Response como una respuesta Json.
    /// </summary>
    /// <param name="Context">
    ///     Información detallada del Request que se está ejecutando.
    /// </param>
    /// <param name="StatusCode">
    ///     Código de error del Response del Request.
    /// </param>
    private static void ConfigureHttpContextResponse(HttpContext Context, int StatusCode)
    {
        Context.Response.ContentType = "application/json";
        Context.Response.StatusCode = StatusCode;
    }

    /// <summary>
    ///     Crea un <see cref="ErrorResponse" /> que será usado por <see cref="IResponse{T}.Data" />.
    /// </summary>
    /// <param name="Context">
    ///     Información detallada del Request que se está ejecutando.
    /// </param>
    /// <param name="StatusCode">
    ///     Código de error del Response del Request. Por defecto tiene un valor de 500.
    /// </param>
    /// <returns>
    ///     Regresa un <see cref="ErrorResponse" /> con información detallada del error del Request.
    /// </returns>
    private async ValueTask<ErrorResponse> CreateHttpContextResponseDataAsync(HttpContext Context, int StatusCode = StatusCodes.Status500InternalServerError)
    {
        ConfigureHttpContextResponse(Context, StatusCode);

        Context.Request.Body.Seek(0, SeekOrigin.Begin);
        using var Reader = new StreamReader(Context.Request.Body, Encoding.UTF8);
        string RequestBody = await Reader.ReadToEndAsync().ConfigureAwait(false);
        Context.Request.Body.Seek(0, SeekOrigin.Begin);

        return new ErrorResponse
        (
            HttpMethod: Context.Request.Method,
            ServerHost: Context.Request.Host.ToString(),
            EndpointPath: Context.Request.Path.ToString(),
            QueryString: Context.Request.QueryString.ToString(),
            RequestBody: JsonNode.Parse(RequestBody)
        );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Exception"></param>
    /// <returns></returns>
    private static ErrorStatus CreateErrorStatusFromException(Exception Exception)
    {
        return new()
        {
            HasError = true,
            MethodId = SysCreditMiddlewareMethodId,
            ErrorCode = InternalServerErrorCode,
            ErrorMessage = Exception.Message,
            ErrorCategory = typeof(SysCreditMiddleware).GetErrorCategory(),
            Extensions =
            {
                [SysCreditConstants.ExceptionTypeKey]       = Exception.GetType().ToString(),
                [SysCreditConstants.ExceptionMessagesKey]   = Exception.GetMessages().ToArray(),
                [SysCreditConstants.ExceptionSourceKey]     = Exception.Source,
                [SysCreditConstants.ExceptionStackTraceKey] = Exception.StackTrace
            }
        };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Response"></param>
    /// <returns></returns>
    private string SerializeResponse(IResponse Response)
    {
        var JsonTypeInfo = JsonOptions.Value.JsonSerializerOptions.TypeInfoResolver!.GetTypeInfo(
            Response.GetType(), JsonOptions.Value.JsonSerializerOptions);

        return JsonSerializer.Serialize(Response, JsonTypeInfo!);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Response"></param>
    /// <returns></returns>
    private string WriteLogErrorResponse(IResponse Response)
    {
        string JsonText = SerializeResponse(Response);
        Logger.LogError(JsonText);
        return JsonText;
    }
}
