using CashFlowControl.Core.Application.DTOs;
using CashFlowControl.Core.Application.Interfaces.Repositories;
using CashFlowControl.Core.Domain.Entities;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace CashFlowControl.DailyConsolidation.Infrastructure.Messaging.Consumers
{
    public class TransactionCreatedConsumer : IConsumer<TransactionCreatedDTO>
    {
        private readonly ILogger<TransactionCreatedConsumer> _logger;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionCreatedConsumer(ILogger<TransactionCreatedConsumer> logger, ITransactionRepository transactionRepository)
        {
            _logger = logger;
            _transactionRepository = transactionRepository;
        }

        public async Task Consume(ConsumeContext<TransactionCreatedDTO> context)
        {
            var message = context.Message;

            _logger.LogInformation("Mensagem recebida: Id {Id}, Amount {Amount}, Type {Type}, CreatedAt {CreatedAt}",
                message.Id, message.Amount, message.Type, message.CreatedAt);

            var transaction = new Transaction
            {
                Id = message.Id,
                Amount = message.Amount,
                Type = message.Type,
                CreatedAt = message.CreatedAt
            };

            await _transactionRepository.CreateTransactionAsync(transaction);
        }
    }
}
