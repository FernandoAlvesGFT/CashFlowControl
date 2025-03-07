using CashFlowControl.Core.Application.Interfaces.Services;
using CashFlowControl.Core.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace CashFlowControl.Core.Application.Services
{
    public class TransactionHttpClientService : ITransactionHttpClientService
    {
        private readonly HttpClient _httpClient;
        private readonly string TransactionApiUrl;

        public TransactionHttpClientService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            TransactionApiUrl = configuration["TransactionApiUrl"] ?? throw new ArgumentNullException("TransactionApiUrl não configurada!");
        }

        public async Task<List<Transaction>?> GetTransactionsAsync()
        {
            var allTransactions = await _httpClient.GetFromJsonAsync<List<Transaction>>(TransactionApiUrl);
            return allTransactions;
        }
        public async Task<List<Transaction>?> GetTransactionsByDateAsync(DateTime date)
        {
            var allTransactions = await _httpClient.GetFromJsonAsync<List<Transaction>>(TransactionApiUrl + $"/{date.Date}");
            return allTransactions;
        }
    }
}
