namespace SysCredit.Api.Tests.IntegrationTests;

using Microsoft.AspNetCore.Mvc.Testing;

public class SysCreditServerFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
}
