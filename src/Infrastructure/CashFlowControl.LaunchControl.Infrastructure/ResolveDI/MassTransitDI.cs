using MassTransit;
using Microsoft.AspNetCore.Builder;

namespace CashFlowControl.LaunchControl.Infrastructure.ResolveDI
{
    public static class MassTransitDI
    {
        public static void Registry(WebApplicationBuilder builder)
        {
            builder.Services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("rabbitmq:5672");
                });
            });
        }
    }
}
