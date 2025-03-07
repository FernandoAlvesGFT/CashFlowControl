using CashFlowControl.Core.Infrastructure.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace CashFlowControl.Core.Infrastructure.Configurations
{
    public static class DatabaseMigrator
    {
        public static void ApplyMigrations(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();


                try
                {
                    InitializeDb(dbContext);

                    dbContext.Database.Migrate();
                }
                catch (Microsoft.Data.SqlClient.SqlException ex)
                {
                    if (ex.Number != 2714)
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    Log.Error($"Error trying to migrate database: {ex.Message}");
                    throw;
                }
            }
        }

        private static void InitializeDb(ApplicationDbContext dbContext)
        {
            var retryCount = 5;
            while (retryCount > 0)
            {
                try
                {
                    dbContext.Database.EnsureCreated();
                    //dbContext.Database.OpenConnection();


                    //dbContext.Database.CloseConnection();
                    return;
                }
                catch
                {
                    retryCount--;
                    Thread.Sleep(5000); // Espera 5 segundos
                }
            }
            throw new Exception("Failed to connect to database.");
        }
    }
}
