using SysCredit.Api.Constants;
using SysCredit.Api.Extensions;

var Builder = WebApplication.CreateBuilder(args);
Builder.AddSysCreditLogging();
Builder.AddSysCreditServices();

var App = Builder.Build();
App.UseHttpLogging();
App.UseSysCreditLog4Net();
App.UseSysCreditSwaggerUI();
App.UseSysCreditMiddlewares();
App.UseHttpsRedirection();
App.UseCors(SysCreditConstants.CorsAllowSpecificOrigins);
App.UseAuthorization();
App.MapControllers();
App.Run();
