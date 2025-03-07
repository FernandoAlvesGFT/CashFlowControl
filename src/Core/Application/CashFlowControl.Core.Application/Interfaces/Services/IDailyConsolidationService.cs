using CashFlowControl.Core.Application.DTOs;

namespace CashFlowControl.Core.Application.Interfaces.Services
{
    public interface IDailyConsolidationService
    {
        Task ProcessTransactionAsync(CreateTransactionDTO transaction);
        Task ConsolidateDailyBalanceAsync(DateTime date);
        Task<decimal?> GetConsolidatedBalanceByDateAsync(DateTime date);
    }
}
