namespace SysCredit.Api.Middlewares;

using Microsoft.AspNetCore.Http;

using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.Exceptions;
using SysCredit.Api.Extensions;

using SysCredit.Helpers;

using System.Data.SqlClient;
using System.Text;
using System.Text.Json;

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
[ErrorCategory(nameof(SysCreditMiddleware))]
[ErrorCodePrefix(InternalServerErrorPrefix)]
public class SysCreditMiddleware(RequestDelegate Next, ILogger<SysCreditMiddleware> Logger)
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
        catch (EndpointFlowException Exception)
        {
            IResponse Response = await CreateHttpContextResponseDataAsync(Context, StatusCodes.Status400BadRequest)!.ToResponseAsync(Exception.Status);
            await Context.Response.WriteAsync(WriteLogErrorResponse(Response));
        }
        catch (SysCreditException Exception) when (Exception.Data.Contains(SysCreditConstants.ErrorStatusKey))
        {
            IResponse Response = await CreateHttpContextResponseDataAsync(Context)!.ToResponseAsync(Exception.Data[SysCreditConstants.ErrorStatusKey].As<ErrorStatus>());
            await Context.Response.WriteAsync(WriteLogErrorResponse(Response));
        }
        catch (ProxyException Exception)
        {
            IResponse Response = await CreateHttpContextResponseDataAsync(Context)!.ToResponseAsync(CreateErrorStatusFromProxyException(Exception));
            await Context.Response.WriteAsync(WriteLogErrorResponse(Response));
        }
        catch (SqlException Exception)
        {
            IResponse Response = await CreateHttpContextResponseDataAsync(Context)!.ToResponseAsync(CreateErrorStatusFromSqlException(Exception));
            await Context.Response.WriteAsync(WriteLogErrorResponse(Response));
        }
        catch (Exception Exception)
        {
            IResponse Response = await CreateHttpContextResponseDataAsync(Context)!.ToResponseAsync(CreateErrorStatusFromException(Exception));
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
    private static async ValueTask<ErrorResponse> CreateHttpContextResponseDataAsync(HttpContext Context, int StatusCode = StatusCodes.Status500InternalServerError)
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
            RequestBody: RequestBody.DeserializeIfNotNullOrEmpty<Dictionary<string, object>>()
        );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Exception"></param>
    /// <returns></returns>
    private static ErrorStatus CreateErrorStatusFromSqlException(SqlException Exception)
    {
        var Status = CreateErrorStatusFromException(Exception);

        Status.ErrorCode = DatabaseProviderErrorCode;
        Status.Extensions["SqlProcedure"] = Exception.Procedure;
        Status.Errors = new Dictionary<string, object?>
        {
            ["SqlErrors"] = Exception.Errors.Cast<SqlError>().Select(SqlError => SqlError.Message).ToArray()
        };

        return Status;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Exception"></param>
    /// <returns></returns>
    private static ErrorStatus CreateErrorStatusFromProxyException(ProxyException Exception)
    {
        var Status = CreateErrorStatusFromException(Exception);

        Status.MethodId = Exception.Data[SysCreditConstants.MethodIdKey]!.ToString();
        Status.ErrorCode = Exception.Data[SysCreditConstants.ErrorCodeKey]!.ToString();
        Status.ErrorCategory = Exception.Data[SysCreditConstants.ErrorCategoryKey]!.ToString();

        return Status;
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
                ["ExceptionType"]       = Exception.GetType().ToString(),
                ["ExceptionMessages"]   = Exception.GetMessages().ToArray(),
                ["ExceptionSource"]     = Exception.Source,
                ["ExceptionStackTrace"] = Exception.StackTrace
            }
        };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Response"></param>
    /// <returns></returns>
    private static string SerializeResponse(IResponse Response)
    {
        var Options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonDefaultNamingPolicy.DefaultNamingPolicy,
            WriteIndented = true
        };

        return JsonSerializer.Serialize(Response, Options);
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
