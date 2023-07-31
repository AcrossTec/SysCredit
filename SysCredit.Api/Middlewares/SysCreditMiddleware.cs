namespace SysCredit.Api.Middlewares;

using Microsoft.AspNetCore.Http;

using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.Exceptions;
using SysCredit.Api.Extensions;
using SysCredit.Api.Helpers;

using System.Reflection;
using System.Text.Json;

using static Constants.ErrorCodePrefix;
using static Constants.ErrorCodeNumber;

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

    [ErrorCode(Prefix = InternalServerErrorPrefix, Codes = new[] { _0001 })]
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

            IResponse Response = await CreateHttpContextResponseData(Context).ToResponseAsync(Ex.Status);

            await Context.Response.WriteAsync(SerializeResponse(Response));
        }
        catch (Exception Ex)
        {
            Logger.LogError(Ex, Ex.Message);
            ConfigureHttpContextResponse(Context);

            IResponse Response = await CreateHttpContextResponseData(Context).ToResponseAsync(new()
            {
                HasError = true,
                ErrorMessage = Ex.Message,
                ErrorCode = MethodInfo.GetCurrentMethod()!.GetErrorCode(ErrorCodeIndex.CodeIndex0),
                ErrorCategory = MethodInfo.GetCurrentMethod()!.GetErrorCategory(),
                Errors =
                {
                    [nameof(Ex.Source)] = new[] { Ex.Source! },
                    [nameof(Ex.StackTrace)] = new[] { Ex.StackTrace! }
                }
            });

            await Context.Response.WriteAsync(SerializeResponse(Response));
        }
    }

    private static void ConfigureHttpContextResponse(HttpContext Context)
    {
        Context.Response.ContentType = "application/json";
        Context.Response.StatusCode = StatusCodes.Status500InternalServerError;
    }

    private static object CreateHttpContextResponseData(HttpContext Context)
    {
        return new
        {
            HTTPMethod = Context.Request.Method,
            Host = Context.Request.Host.ToString(),
            Path = Context.Request.Path.ToString(),
            QueryString = Context.Request.QueryString.ToString()
        };
    }

    private static string SerializeResponse(IResponse Response)
    {
        var Options = new JsonSerializerOptions { PropertyNamingPolicy = JsonDefaultNamingPolicy.DefaultNamingPolicy };
        return JsonSerializer.Serialize(Response, Options);
    }
}
