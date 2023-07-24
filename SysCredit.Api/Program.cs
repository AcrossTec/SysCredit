using Microsoft.AspNetCore.Mvc;

using SysCredit.Api;
using SysCredit.Api.Middlewares;
using SysCredit.Api.Services;
using SysCredit.Api.Stores;

var Builder = WebApplication.CreateBuilder(args);

// Add services to the container.

Builder.Services.AddControllers()
    .AddJsonOptions(Options =>
    {
        Options.JsonSerializerOptions.PropertyNamingPolicy = JsonDefaultNamingPolicy.DefaultNamingPolicy;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
Builder.Services.AddEndpointsApiExplorer();
Builder.Services.AddSwaggerGen();

Builder.Services.AddScoped(typeof(IStore<>), typeof(Store<>));
Builder.Services.AddScoped<ICustomerService, CustomerService>();

Builder.Services.AddOptions<SysCreditOptions>()
    .Configure<IConfiguration>(static (Option, Config) => Option.ConnectionString = Config.GetConnectionString("SysCreditConnectionString")!);

Builder.Services.Configure<ApiBehaviorOptions>(Option =>
{
    Option.SuppressModelStateInvalidFilter = true;
});

var App = Builder.Build();

// Configure the HTTP request pipeline.
if (App.Environment.IsDevelopment())
{
    App.UseSwagger();
    App.UseSwaggerUI();
}

App.UseMiddleware<SysCreditMiddleware>();
App.UseHttpsRedirection();
App.UseAuthorization();
App.MapControllers();
App.Run();
