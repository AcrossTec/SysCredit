using SysCredit.Api.Constants;
using SysCredit.Api.Extensions;
using SysCredit.Api.Patchers;

using System.Runtime.CompilerServices;

var Builder = WebApplication.CreateBuilder(args);
Builder.AddSysCreditLogging();
Builder.AddSysCreditServices();
Builder.AddSysCreditAuthorization();

var App = Builder.Build();
App.UseHttpLogging();
App.UseSysCreditSwaggerUI();
App.UseSysCreditMiddlewares();
App.UseHttpsRedirection();
App.UseCors(SysCreditConstants.CorsAllowSpecificOrigins);
App.UseAuthentication();
App.UseAuthorization();
App.MapControllers();
App.Run();

public partial class Program
{
    [ModuleInitializer]
    public static void ModuleInitializer()
    {
        SysCreditApiPatcher.PatchAll();
    }
}
