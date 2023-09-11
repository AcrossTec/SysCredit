namespace SysCredit.Api.Extensions;

using Microsoft.Extensions.Logging;

using SysCredit.Api.Middlewares;

/// <summary>
/// 
/// </summary>
public static class WebApplicationExtensions
{
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

    /// <summary>
    /// 
    /// </summary>
    /// <param name="App"></param>
    /// <returns></returns>
    public static WebApplication UseSysCreditSwaggerUI(this WebApplication App)
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
    public static WebApplication UseSysCreditMiddlewares(this WebApplication App)
    {
        App.UseMiddleware<SysCreditMiddleware>();
        return App;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    ///     Custom configuration using Log4NetProviderOptions
    ///     https://github.com/huorswords/Microsoft.Extensions.Logging.Log4Net.AspNetCore/blob/develop/doc/CONFIG.md
    ///
    ///     Apache log4net™ Manual - Configuration
    ///     https://logging.apache.org/log4net/release/manual/configuration.html
    /// </remarks>
    /// <param name="App"></param>
    /// <returns></returns>
    public static WebApplication UseSysCreditLog4Net(this WebApplication App)
    {
        var LoggerFactory = App.Services.GetRequiredService<ILoggerFactory>();

        LoggerFactory.AddLog4Net(new Log4NetProviderOptions
        {
            Log4NetConfigFileName = "Log4Net.config",
            Watch = true
        });

        return App;
    }
}
