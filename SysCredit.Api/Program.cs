using SysCredit.Api.Constants;
using SysCredit.Api.Extensions;

var Builder = WebApplication.CreateBuilder(args);
Builder.Services.AddSysCreditEndpoints();
Builder.Services.AddSysCreditSwaggerGen();
Builder.Services.AddSysCreditStores();
Builder.Services.AddSysCreditServices();
Builder.Services.AddSysCreditOptions();

var App = Builder.Build();
App.ConfigureHttpRequestPipeline();
App.UseSysCreditMiddlewares();
App.UseHttpsRedirection();
App.UseCors(SysCreditConstants.CorsAllowSpecificOrigins);
App.UseAuthorization();
App.MapControllers();
App.Run();
