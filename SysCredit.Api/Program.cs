using SysCredit.Api;

var Builder = WebApplication.CreateBuilder(args);

var Startup = new Startup(Builder.Configuration);
Startup.ConfigureServices(Builder.Services);

var App = Builder.Build();

Startup.Configure(App, App.Environment);
App.MapControllers();
App.Run();
