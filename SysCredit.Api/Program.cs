using SysCredit.Api.Constants;
using SysCredit.Api.Extensions;
using SysCredit.Api.Patchers;

var Builder = WebApplication.CreateBuilder(args);
Builder.AddSysCreditLogging();
Builder.AddSysCreditServices();
Builder.AddSysCreditAuthorization();

SysCreditApiPatcher.PatchAll();

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

public partial class Program;