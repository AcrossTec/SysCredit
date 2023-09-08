namespace SysCredit.Api.Extensions;

using SysCredit.Api.Middlewares;

/// <summary>
/// 
/// </summary>
public static class ApplicationBuilderExtensions
{
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

    /// <summary>
    /// 
    /// </summary>
    /// <param name="App"></param>
    /// <returns></returns>
    public static WebApplication ConfigureHttpRequestPipeline(this WebApplication App)
    {
        if (App.Environment.IsDevelopment())
        {
            App.UseSwagger();
            App.UseSwaggerUI();
        }

        return App;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="App"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseSysCreditMiddlewares(this IApplicationBuilder App)
    {
        App.UseMiddleware<SysCreditMiddleware>();
        return App;
    }
}
