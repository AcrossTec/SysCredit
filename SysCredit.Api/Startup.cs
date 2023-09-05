namespace SysCredit.Api;

using SysCredit.Api.Constants;
using SysCredit.Api.Extensions;

public class Startup
{
    public Startup(IConfiguration Configuration)
    {
        this.Configuration = Configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection Services)
    {
        Services.AddSwaggerDocumentation();
        Services.AddAuth(Configuration);
        Services.AddSysCreditEndpoints();
        Services.AddSysCreditSwaggerGen();
        Services.AddSysCreditStores();
        Services.AddSysCreditServices();
        Services.AddSysCreditOptions();
    }

    public void Configure(IApplicationBuilder App, IWebHostEnvironment Environment)
    {
        App.ConfigureHttpRequestPipeline(Environment);
        App.UseSysCreditMiddlewares();
        App.UseHttpsRedirection();
        App.UseCors(SysCreditConstants.CorsAllowSpecificOrigins);
        App.UseAuthorization();
    }
}
