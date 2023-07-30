namespace SysCredit.Api.Extensions;

using SysCredit.Api.Middlewares;

public static class ApplicationBuilderExtensions
{
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    public static WebApplication ConfigureHttpRequestPipeline(this WebApplication App)
    {
        return (WebApplication)App.ConfigureHttpRequestPipeline(App.Environment);
    }

    public static IApplicationBuilder ConfigureHttpRequestPipeline(this IApplicationBuilder App, IWebHostEnvironment Environment)
    {
        if (Environment.IsDevelopment())
        {
            App.UseSwagger();
            App.UseSwaggerUI();
        }

        return App;
    }

    public static IApplicationBuilder UseSysCreditMiddlewares(this IApplicationBuilder App)
    {
        App.UseMiddleware<SysCreditMiddleware>();
        return App;
    }
}
