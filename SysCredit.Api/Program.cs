using log4net.Config;

using Microsoft.AspNetCore.Mvc;

using SysCredit.Api.Attributes;
using SysCredit.Api.Extensions;
using SysCredit.Api.Patchers;

using System.Runtime.CompilerServices;

using static SysCredit.Api.Constants.SysCreditConstants;

[assembly: ApiController]
[assembly: ErrorCodeRange(MinCodeNumber: 0, MaxCodeNumber: 300)]
[assembly: XmlConfigurator(ConfigFile = Log4NetConfigFile, Watch = true)]

var Builder = WebApplication.CreateBuilder(args);
Builder.AddSysCreditLogging();
Builder.AddSysCreditServices();
Builder.AddSysCreditAuthentication();

var App = Builder.Build();
App.UseHttpLogging();
App.UseSysCreditSwaggerUI();
App.UseSysCreditMiddlewares();
App.UseHttpsRedirection();
App.UseCors(CorsAllowSpecificOrigins);
App.UseAuthentication();
App.UseAuthorization();
App.MapControllers();
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
        SysCreditApiPatcher.PatchAll();
    }
}
