using CashFlowControl.DailyConsolidation.Infrastructure.Messaging.Consumers;
using MassTransit;
using Microsoft.AspNetCore.Builder;

namespace CashFlowControl.DailyConsolidation.Infrastructure.ResolveDI
{
    public static class MassTransitDI
    {
        public static void Registry(WebApplicationBuilder builder)
        {
            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<TransactionCreatedConsumer>(); // Registrando o consumidor

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("rabbitmq:5672");
                    cfg.ReceiveEndpoint("transaction-created-queue", e =>
                    {
                        e.ConfigureConsumer<TransactionCreatedConsumer>(context);
                    });
                });
            });
        }
    }
}
