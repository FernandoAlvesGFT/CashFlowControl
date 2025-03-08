using CashFlowControl.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashFlowControl.DailyConsolidation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsolidatedBalanceController : ControllerBase
    {
        private readonly IDailyConsolidationService _dailyConsolidationService;

        public ConsolidatedBalanceController(IDailyConsolidationService dailyConsolidationService)
        {
            _dailyConsolidationService = dailyConsolidationService;
        }

        [Authorize]
        [HttpGet("{date}")]
        public async Task<ActionResult<decimal>> GetConsolidatedBalance(DateTime date)
        {
            var balance = await _dailyConsolidationService.GetConsolidatedBalanceByDateAsync(date.Date);
            if (balance == null)
                return NotFound("No balance found for the given date.");

            return Ok(balance);
        }
    }
}
