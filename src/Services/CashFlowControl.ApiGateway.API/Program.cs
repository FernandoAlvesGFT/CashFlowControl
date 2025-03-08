using CashFlowControl.ApiGateway.API.Configurations;
using CashFlowControl.ApiGateway.API.Configurations.ResolveDI;
using CashFlowControl.Core.Infrastructure.Configurations.ResolveDI;
using CashFlowControl.Core.Infrastructure.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

ConfigureKestrel.Configure(builder);

SerilogConfig.Configuration();

builder.Host.UseSerilog();

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true).Build();

TokenJwtDI.RegistryConsumer(builder);
builder.Services.AddOcelot();

SwaggerDI.Registry(builder);

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

SwaggerConfig.Configure(app);

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseOcelot().Wait();

app.MapControllers();

app.Run();
