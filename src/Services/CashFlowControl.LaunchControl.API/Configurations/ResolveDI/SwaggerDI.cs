using Microsoft.OpenApi.Models;

namespace CashFlowControl.LaunchControl.API.Configurations.ResolveDI
{
    public static class SwaggerDI
    {
        public static void Registry(WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Launch Control API",
                    Version = "v1",
                    Description = "API responsável pelos lançamentos dos controles."
                });
            });
        }
    }
}
