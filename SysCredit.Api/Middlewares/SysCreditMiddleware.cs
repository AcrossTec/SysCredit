namespace SysCredit.Api.Middlewares;

using SysCredit.Api.Helpers;

using System.Text.Json;

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

    public async Task InvokeAsync(HttpContext Context)
    {
        try
        {
            await Next(Context);
        }
        catch (Exception Ex)
        {
            Logger.LogError(Ex, Ex.Message);
            Context.Response.ContentType = "application/json";
            Context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            IResponse Response = new Response
            {
                Status = new ErrorStatus
                {
                    HasError = true,
                    Message = Ex.Message,
                    ErrorCode = Ex.HResult,
                    ErrorCategory = Ex.GetType().Name,
                    Errors = new()
                    {
                        [nameof(Ex.Source)] = new[] { Ex.Source! },
                        [nameof(Ex.StackTrace)] = new[] { Ex.StackTrace! }
                    }
                }
            };

            var Options = new JsonSerializerOptions { PropertyNamingPolicy = JsonDefaultNamingPolicy.DefaultNamingPolicy };
            var Json = JsonSerializer.Serialize(Response, Options);
            await Context.Response.WriteAsync(Json);
        }
    }
}
