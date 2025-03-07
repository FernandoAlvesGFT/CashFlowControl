using Microsoft.OpenApi.Models;

namespace CashFlowControl.DailyConsolidation.API.Configurations.ResolveDI
{
    public static class SwaggerDI
    {
        public static void Registry(WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Daily Consolidation API",
                    Version = "v1",
                    Description = "API responsável por consolidar lançamentos diários e calcular o saldo."
                });
            });
        }
    }
}
