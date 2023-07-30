using SysCredit.Api.Extensions;

var Builder = WebApplication.CreateBuilder(args);

Builder.Services.AddSysCreditEndpoints();
Builder.Services.AddSwaggerGen();
Builder.Services.AddSysCreditStores();
Builder.Services.AddSysCreditServices();
Builder.Services.AddSysCreditOptions();

var App = Builder.Build();

App.ConfigureHttpRequestPipeline();
App.UseSysCreditMiddlewares();
App.UseHttpsRedirection();
App.UseAuthorization();
App.MapControllers();
App.Run();
