namespace SysCredit.Api;

/// <summary>
/// 
/// </summary>
/// <param name="Configuration"></param>
[Obsolete]
public class Startup(IConfiguration Configuration)
{
    /// <summary>
    /// 
    /// </summary>
    public IConfiguration Configuration { get; } = Configuration;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Services"></param>
    public void ConfigureServices(IServiceCollection Services)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="App"></param>
    /// <param name="Environment"></param>
    public void Configure(IApplicationBuilder App, IWebHostEnvironment Environment)
    {
    }
}
