namespace SysCredit.Api.Tests.IntegrationTests;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
///     Integration tests in ASP.NET Core
///     https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-7.0
/// </summary>
/// <typeparam name="TProgram"></typeparam>
public class SysCreditServerFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder Builder)
    {
        Builder.ConfigureServices(delegate (WebHostBuilderContext Context, IServiceCollection Services)
        {

        });

        Builder.UseEnvironment("Development");
    }
}
