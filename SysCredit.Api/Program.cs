using System.Runtime.CompilerServices;

using log4net.Config;

using SysCredit.Api.Attributes;
using SysCredit.Api.Endpoints;
using SysCredit.Api.Extensions;

using static SysCredit.Api.Constants.SysCreditConstants;

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
App.UseHttpLogging();
App.UseHttpsRedirection();
App.UseSysCreditSwaggerUI();
App.UseSysCreditMiddlewares();
App.UseCors(CorsAllowSpecificOrigins);
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
    }
}
