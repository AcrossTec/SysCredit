namespace SysCredit.Api.Extensions;

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
}
