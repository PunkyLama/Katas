using Domain.Ports.Driving;
using Microsoft.AspNetCore.Mvc;

namespace Web.API
{
    [Route("Accounts")]
    [ApiController]
    public class Controller : ControllerBase
    {
        private readonly IAccountPort _accountPort;

        public Controller(IAccountPort accountPort)
        {
            _accountPort = accountPort;
        }

        [HttpPost("{id}/deposit")]
        public async Task<IActionResult> Deposit([FromRoute] int id, float amount)
        {
            var account = await _accountPort.DepositByIdAsync(id, amount);

            // Vérifie si l'opération a été rejetée
            if (account.TransactionHistories.Last().TransactionStatusString == "Rejected")
            {
                // HTTP 400 Bad Request avec le message d'erreur
                return BadRequest(new { Message = $"Deposite operation rejected. negative entry." });
            }

            return Ok(account);
        }

        [HttpGet("{id}/statement")]
        public async Task<IActionResult> GetStatement([FromRoute] int id)
        {
            var transactions = await _accountPort.GetStatementByIdAsync(id);
            return Ok(transactions);
        }

        [HttpPost("{id}/withdraw")]
        public async Task<IActionResult> Withdraw([FromRoute] int id, float amount)
        {
            var account = await _accountPort.WithdrawByIdAsync(id, amount);

            // Vérifie si l'opération a été rejetée
            if (account.TransactionHistories.Last().TransactionStatusString == "Rejected")
            {
                // HTTP 400 Bad Request avec le message d'erreur
                return BadRequest(new { Message = $"Withdrawal operation rejected. Insufficient funds." });
            }

            return Ok(account);
        }
    }
}
