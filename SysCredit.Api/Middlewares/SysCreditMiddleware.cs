﻿namespace SysCredit.Api.Middlewares;

using Microsoft.AspNetCore.Http;

using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.Exceptions;
using SysCredit.Api.Extensions;

using SysCredit.Helpers;

using System.Data.SqlClient;
using System.Text.Json;

using static Constants.ErrorCodePrefix;
using static Constants.ErrorCodes;

/// <summary>
/// 
/// </summary>
/// <param name="Next"></param>
/// <param name="Logger"></param>
[ErrorCategory(nameof(SysCreditMiddleware))]
[ErrorCodePrefix(InternalServerErrorPrefix)]
public class SysCreditMiddleware(RequestDelegate Next, ILogger<SysCreditMiddleware> Logger)
{
    /// <summary>
    /// 
    /// </summary>
    public const string SysCreditMiddlewareMethodId = "73E66405-D1D0-44D0-8EAB-9AC7D08742A9";

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Context"></param>
    /// <returns></returns>
    [MethodId(SysCreditMiddlewareMethodId)]
    public async Task InvokeAsync(HttpContext Context)
    {
        try
        {
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
    /// 
    /// </summary>
    /// <param name="Context"></param>
    /// <param name="StatusCode"></param>
    private static void ConfigureHttpContextResponse(HttpContext Context, int StatusCode)
    {
        Context.Response.ContentType = "application/json";
        Context.Response.StatusCode = StatusCode;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Context"></param>
    /// <param name="StatusCode"></param>
    /// <returns></returns>
    private static async ValueTask<object> CreateHttpContextResponseDataAsync(HttpContext Context, int StatusCode = StatusCodes.Status500InternalServerError)
    {
        ConfigureHttpContextResponse(Context, StatusCode);

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
