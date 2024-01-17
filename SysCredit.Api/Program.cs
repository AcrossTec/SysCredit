using System.Runtime.CompilerServices;

using log4net.Config;

using Microsoft.AspNetCore.Mvc;

using SysCredit.Api.Attributes;
using SysCredit.Api.Endpoints;
using SysCredit.Api.Extensions;

using static SysCredit.Api.Constants.SysCreditConstants;

[assembly: ApiController]
[assembly: ErrorCodeRange(MinCodeNumber: 0, MaxCodeNumber: 300)]
[assembly: XmlConfigurator(ConfigFile = Log4NetConfigFile, Watch = true)]

var Builder = WebApplication
    .CreateSlimBuilder(args)
    .ConfigureHttpJsonOptions()
    .AddSysCreditLogging()
    .AddSysCreditServices()
    .AddSysCreditAuthentication();

Builder.WebHost.UseKestrelHttpsConfiguration();
Builder.WebHost.UseQuic();

var App = Builder.Build();
App.UseRouting();

// App.UseAuthentication();
// App.UseAuthorization();

App.UseHttpLogging();
App.UseHttpsRedirection();

App.UseSysCreditSwaggerUI();
App.UseSysCreditMiddlewares();

App.UseCors(CorsAllowSpecificOrigins);

var LoanTypeEndpoints = App.MapGroup("/Api/LoanType");
LoanTypeEndpoints.MapGet("/", () => new LoanTypeInfo[]
{
    new() { LoanTypeId = 1, Name = "Loan Type #1" },
    new() { LoanTypeId = 2, Name = "Loan Type #2" },
    new() { LoanTypeId = 3, Name = "Loan Type #3" },
    new() { LoanTypeId = 4, Name = "Loan Type #4" },
    new() { LoanTypeId = 5, Name = "Loan Type #5" },
})
.WithName("GetLoanTypes")
.WithOpenApi();

App.MapPaymentFrequencyEndpoints();
App.Run();

/// <summary>
///     Punto de entrada de la aplicación SysCredit.
/// </summary>
public partial class Program
{
    /// <summary>
    ///     Contructor del Módulo SysCredit.
    /// </summary>
    [ModuleInitializer]
    public static void ModuleInitializer()
    {
        // No compatible con AOT
        // SysCreditApiPatcher.PatchAll();
    }
}
