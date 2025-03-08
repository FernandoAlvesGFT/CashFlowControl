using CashFlowControl.Core.Application.DTOs;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace CashFlowControl.DailyConsolidation.Infrastructure.Messaging.Consumers
{
    public class TransactionFailedConsumer : IConsumer<TransactionCreatedDTO>
    {
        private readonly ILogger<TransactionFailedConsumer> _logger;
        private readonly IBus _messageBus;
        public TransactionFailedConsumer(ILogger<TransactionFailedConsumer> logger, IBus messageBus)
        {
            _logger = logger;
            _messageBus = messageBus;
        }

        public async Task Consume(ConsumeContext<TransactionCreatedDTO> context)
        {
            var message = context.Message;

            _logger.LogError("Transaction processing failed and moved to DLQ: Amount {Amount}, Type {Type}, CreatedAt {CreatedAt}",
                message.Amount, message.Type, message.CreatedAt);

            await _messageBus.Publish(message);

            await Task.CompletedTask;
        }
    }
}
