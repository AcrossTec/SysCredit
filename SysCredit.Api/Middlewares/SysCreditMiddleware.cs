namespace SysCredit.Api.Middlewares;

using Microsoft.AspNetCore.Http;

using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.Exceptions;
using SysCredit.Api.Extensions;

using SysCredit.Helpers;

using System.Data.SqlClient;
using System.Text.Json;

using static Constants.ErrorCodeNumber;
using static Constants.ErrorCodePrefix;

[ErrorCategory(ErrorCategories.InternalServerError)]
public class SysCreditMiddleware
{
    private readonly RequestDelegate Next;
    private readonly ILogger<SysCreditMiddleware> Logger;
    private readonly IHostEnvironment Environment;

    public SysCreditMiddleware(RequestDelegate Next, IHostEnvironment Environment, ILogger<SysCreditMiddleware> Logger)
    {
        this.Next = Next;
        this.Environment = Environment;
        this.Logger = Logger;
    }

    [MethodId("73E66405-D1D0-44D0-8EAB-9AC7D08742A9")]
    [ErrorCode(Prefix: InternalServerErrorPrefix, Codes: new[] { _0001, _0002 })]
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

            Response.Status.ErrorCode = typeof(SysCreditMiddleware).GetErrorCode("73E66405-D1D0-44D0-8EAB-9AC7D08742A9", ErrorCodeIndex.CodeIndex1);
            Response.Status.Errors[nameof(Ex.Procedure)] = new[] { Ex.Procedure };
            Response.Status.Errors["DatabaseErrors"] = Ex.Errors.Cast<SqlError>().Select(SqlError => SqlError.Message).ToArray();

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
            HTTPMethod = Context.Request.Method,
            Host = Context.Request.Host.ToString(),
            Path = Context.Request.Path.ToString(),
            QueryString = Context.Request.QueryString.ToString(),
            Body = await BodyReader.ReadToEndAsync(),
        };
    }

    private static ErrorStatus CreateErrorStatusFromException(Exception Ex)
    {
        return new()
        {
            HasError = true,
            ErrorMessage = Ex.Message,
            ErrorCode = typeof(SysCreditMiddleware).GetErrorCode("73E66405-D1D0-44D0-8EAB-9AC7D08742A9", ErrorCodeIndex.CodeIndex0),
            ErrorCategory = typeof(SysCreditMiddleware).GetErrorCategory(),
            MethodId = "73E66405-D1D0-44D0-8EAB-9AC7D08742A9",
            Errors =
            {
                ["ExceptionType"]       = new[] { Ex.GetType().Name },
                ["ExceptionCode"]       = new[] { Ex.HResult.ToString() },
                ["ExceptionMessages"]   = Ex.GetMessages().ToArray(),
                [nameof(Ex.Source)]     = new[] { Ex.Source! },
                [nameof(Ex.StackTrace)] = new[] { Ex.StackTrace! }
            }
        };
    }

    private static string SerializeResponse(IResponse Response)
    {
        var Options = new JsonSerializerOptions { PropertyNamingPolicy = JsonDefaultNamingPolicy.DefaultNamingPolicy };
        return JsonSerializer.Serialize(Response, Options);
    }
}
