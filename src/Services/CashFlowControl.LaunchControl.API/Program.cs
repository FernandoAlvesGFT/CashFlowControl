using Serilog;
using CashFlowControl.LaunchControl.Infrastructure.ResolveDI;
using CashFlowControl.LaunchControl.API.Configurations.ResolveDI;
using CashFlowControl.LaunchControl.Infrastructure.Logging;
using CashFlowControl.LaunchControl.API.Configurations;
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

// Torna o Program acessível para os testes de integração
public partial class Program { }