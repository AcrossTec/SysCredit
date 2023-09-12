namespace SysCredit.Api.Middlewares;

using Microsoft.AspNetCore.Http;

using SysCredit.Api.Attributes;
using SysCredit.Api.Exceptions;
using SysCredit.Api.Extensions;

using SysCredit.Helpers;

using System.Data.SqlClient;
using System.Text.Json;

using static Constants.ErrorCodeNumber;
using static Constants.ErrorCodePrefix;

/// <summary>
/// 
/// </summary>
/// <param name="Next"></param>
/// <param name="Environment"></param>
/// <param name="Logger"></param>
[ErrorCategory(nameof(SysCreditMiddleware))]
public class SysCreditMiddleware(RequestDelegate Next, ILogger<SysCreditMiddleware> Logger)
{
    [MethodId("73E66405-D1D0-44D0-8EAB-9AC7D08742A9")]
    public async Task InvokeAsync(HttpContext Context)
    {
        try
        {
            await Next(Context);
        }
        catch (SysCreditException Ex)
        {
            Logger.LogError(Ex, Ex.Message);
            ConfigureHttpContextResponse(Context);

            IResponse Response = await CreateHttpContextResponseDataAsync(Context)!.ToResponseAsync(Ex.Status);

            await Context.Response.WriteAsync(SerializeResponse(Response));
        }
        catch (SqlException Ex)
        {
            Logger.LogError(Ex, Ex.Message);
            ConfigureHttpContextResponse(Context);

            IResponse Response = await CreateHttpContextResponseDataAsync(Context)!.ToResponseAsync(CreateErrorStatusFromException(Ex));

            Response.Status.ErrorCode = $"{InternalServerErrorPrefix}{_0002}";
            Response.Status.Errors!["Procedure"] = Ex.Procedure;
            Response.Status.Errors!["SqlErrors"] = Ex.Errors.Cast<SqlError>().Select(SqlError => SqlError.Message).ToArray();

            await Context.Response.WriteAsync(SerializeResponse(Response));
        }
        catch (Exception Ex)
        {
            Logger.LogError(Ex, Ex.Message);
            ConfigureHttpContextResponse(Context);

            IResponse Response = await CreateHttpContextResponseDataAsync(Context)!.ToResponseAsync(CreateErrorStatusFromException(Ex));
            await Context.Response.WriteAsync(SerializeResponse(Response));
        }
    }

    private static void ConfigureHttpContextResponse(HttpContext Context)
    {
        Context.Response.ContentType = "application/json";
        Context.Response.StatusCode = StatusCodes.Status500InternalServerError;
    }

    private static async ValueTask<object> CreateHttpContextResponseDataAsync(HttpContext Context)
    {
        using var BodyReader = new StreamReader(Context.Request.BodyReader.AsStream());

        return new
        {
            HttpMethod = Context.Request.Method,
            Host = Context.Request.Host.ToString(),
            Path = Context.Request.Path.ToString(),
            QueryString = Context.Request.QueryString.ToString(),
            Body = await BodyReader.ReadToEndAsync(),
        };
    }

    private ErrorStatus CreateErrorStatusFromException(Exception Ex)
    {
        return new()
        {
            HasError = true,
            ErrorMessage = Ex.Message,
            ErrorCode = $"{InternalServerErrorPrefix}{_0001}",
            ErrorCategory = GetType().GetErrorCategory(),
            MethodId = "73E66405-D1D0-44D0-8EAB-9AC7D08742A9",
            Errors = new Dictionary<string, object?>(StringComparer.Ordinal)
            {
                ["ExceptionType"] = Ex.GetType().Name,
                ["ExceptionCode"] = Ex.HResult,
                ["ExceptionMessages"] = Ex.GetMessages().ToArray(),
                ["ExceptionSource"] = Ex.Source,
                ["ExceptionStackTrace"] = Ex.StackTrace
            }
        };
    }

    private static string SerializeResponse(IResponse Response)
    {
        var Options = new JsonSerializerOptions { PropertyNamingPolicy = JsonDefaultNamingPolicy.DefaultNamingPolicy };
        return JsonSerializer.Serialize(Response, Options);
    }
}
