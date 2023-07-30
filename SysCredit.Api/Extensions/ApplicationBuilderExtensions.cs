namespace SysCredit.Api.Extensions;

using SysCredit.Api.Middlewares;

public static class ApplicationBuilderExtensions
{
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    public static WebApplication ConfigureHttpRequestPipeline(this WebApplication App)
    {
        if (App.Environment.IsDevelopment())
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
