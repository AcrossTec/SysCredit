namespace SysCredit.Api.Tests.IntegrationTests;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using SysCredit.DataTransferObject.Commons;
using SysCredit.Helpers;

public class LoanEndPointTests : IClassFixture<SysCreditServerFactory<Program>>
{
    private readonly HttpClient Client;
    private readonly SysCreditServerFactory<Program> ServerFactory;

    public LoanEndPointTests(SysCreditServerFactory<Program> ServerFactory)
    {
        this.ServerFactory = ServerFactory;

        this.Client = ServerFactory
            .WithWebHostBuilder(Builder => Builder.ConfigureTestServices(ConfigureTestServices))
            .CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
    }

    /// <summary>
    ///     Inject mock services
    ///     https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-7.0#inject-mock-services
    /// </summary>
    /// <param name="Context"></param>
    /// <param name="Services"></param>
    private void ConfigureTestServices(IServiceCollection Services)
    {
    }

    [Fact]
    public async Task Get_LoanByLoanId()
    {
        HttpResponseMessage HttpResponse = await Client.GetAsync("/Api/Loan/1");
        HttpResponse.EnsureSuccessStatusCode();

        Assert.Equal("application/json; charset=utf-8", HttpResponse.Content.Headers.ContentType!.ToString());

        var Json = await HttpResponse.Content.ReadAsStringAsync();
        Assert.NotEmpty(Json);

        var Response = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResult<List<ReferenceInfo>>>(Json);

        Assert.NotNull(Response);
        Assert.NotNull(Response.Status);
        Assert.False(Response.Status.HasError);
        Assert.NotNull(Response.Data);
        Assert.NotEmpty(Response.Data);
    }
}
