namespace SysCredit.Api.Tests.IntegrationTests;

using Microsoft.AspNetCore.Mvc.Testing;

public class LoanTypeEndPointTests : IClassFixture<SysCreditServerFactory<Program>>
{
    private readonly HttpClient Client;
    private readonly SysCreditServerFactory<Program> ServerFactory;

    public LoanTypeEndPointTests(SysCreditServerFactory<Program> ServerFactory)
    {
        this.ServerFactory = ServerFactory;
        this.Client = ServerFactory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }
}
