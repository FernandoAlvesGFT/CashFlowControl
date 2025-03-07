using Serilog;
using CashFlowControl.DailyConsolidation.Infrastructure.ResolveDI;
using CashFlowControl.DailyConsolidation.API.Configurations.ResolveDI;
using CashFlowControl.DailyConsolidation.Infrastructure.Logging;
using CashFlowControl.DailyConsolidation.API.Configurations;
using CashFlowControl.Core.Application.ResolveDI;
using CashFlowControl.Core.Infrastructure.Configurations.ResolveDI;
using CashFlowControl.Core.Infrastructure.Configurations;

var builder = WebApplication.CreateBuilder(args);

ConfigureKestrel.Configure(builder);

SerilogConfig.Configuration();

builder.Host.UseSerilog();

DatabaseDI.Registry(builder);

builder.Services.AddEndpointsApiExplorer();

SwaggerDI.Registry(builder);

MassTransitDI.Registry(builder);

builder.Services.AddControllers();

ResolveServicesDI.RegistryServices(builder);

ResolveRepositoriesDI.RegistryRepositories(builder);

var app = builder.Build();

DatabaseMigrator.ApplyMigrations(app);

SwaggerConfig.Configure(app);

app.UseSerilogRequestLogging();

app.UseAuthorization();
app.MapControllers();

app.Run();