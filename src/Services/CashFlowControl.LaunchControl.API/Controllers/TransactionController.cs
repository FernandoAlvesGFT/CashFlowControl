using CashFlowControl.Core.Application.DTOs;
using CashFlowControl.Core.Application.Interfaces.Services;
using CashFlowControl.Core.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashFlowControl.LaunchControl.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionDTO createTransaction)
        {
            if (createTransaction.Amount <= 0 || (!createTransaction.Type.Equals(TransactionType.Credit.ToString()) && !createTransaction.Type.Equals(TransactionType.Debit.ToString())))
                return BadRequest("Invalid transaction type or amount.");

            try
            {
                var transactionCreated = await _transactionService.CreateTransactionAsync(createTransaction);
                if (transactionCreated == null)
                    return StatusCode(500, new { Message = "Error publishing transaction to message queue." });

                return Accepted(new { Message = "Transaction is being processed", Amount = transactionCreated.Amount, Type = transactionCreated.Type, CreatedAt = transactionCreated.CreatedAt });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "An unexpected error occurred.",
                    Details = ex.Message
                });
            }
        }

        [AllowAnonymous]
        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetTransactionById(Guid id)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(id);
            if (transaction == null)
                return NotFound();

            return Ok(transaction);
        }

        [AllowAnonymous]
        [HttpGet("date/{date}")]
        public async Task<IActionResult> GetTransactionByDate(DateTime date)
        {
            var transaction = await _transactionService.GetTransactionByDateAsync(date);
            if (transaction == null)
                return NotFound();

            return Ok(transaction);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllTransactions()
        {
            var transactions = await _transactionService.GetAllTransactionsAsync();
            if (transactions == null)
                return NotFound();

            return Ok(transactions);
        }
    }
}