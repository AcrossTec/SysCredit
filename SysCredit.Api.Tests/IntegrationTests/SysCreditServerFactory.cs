namespace SysCredit.Api.Tests.IntegrationTests;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
///     Integration tests in ASP.NET Core
///     https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-7.0
///     
///     Testing Controllers with Unit Tests and Moq in ASP.NET Core
///     https://code-maze.com/unit-testing-controllers-aspnetcore-moq/
///     
///     Integration Testing in ASP.NET Core
///     https://code-maze.com/aspnet-core-integration-testing/
/// </summary>
/// <typeparam name="TProgram"></typeparam>
public class SysCreditServerFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder Builder)
    {
        Builder.UseEnvironment("Development");
    }
}
