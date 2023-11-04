using Domain.Commands;
using Domain.Injection;
using Domain.Models;
using Domain.Ports.Driving;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Web.API.Mapper;
using Web.API.Models.Requests;
using Web.API.Models.Responses;

namespace Web.API
{
    [Route("Accounts")]
    [ApiController]
    public class Controller : ControllerBase
    {
        private readonly IMediatr _mediator;
        private readonly AccountAPIMapper _accountAPIMapper = new AccountAPIMapper();
        private readonly StatementAPIMapper _statementAPIMapper = new StatementAPIMapper();
        private readonly DepositRequestMapper _depositRequestMapper = new DepositRequestMapper();
        private readonly StatementRequestMapper _statementRequestMapper = new StatementRequestMapper();
        private readonly BalanceRequestMapper _balanceRequestMapper = new BalanceRequestMapper();
        private readonly WithdrawRequestMapper _withdrawRequestMapper = new WithdrawRequestMapper();

        public Controller(IMediatr mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{id}/deposit")]
        public async Task<IActionResult> Deposit([FromRoute] int id, [Required] float amount)
        {
            try
            {
                var request = new DepositRequest
                {
                    Id = id,
                    Amount = amount
                };
                var command = _depositRequestMapper.MapFrom(request);
                var account = await _mediator.SendAsync(command);
                var result = _accountAPIMapper.MapTo(account);

                if (result == null)
                {
                    return NotFound(new { Message = "Account not found." });
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

        [HttpGet("{id}/statement")]
        public async Task<IActionResult> GetStatement([FromRoute] int id, int element)
        {
            try
            {
                var result = new List<StatementReponse>();
                var request = new StatementRequest
                {
                    Id = id,
                    Element = element
                };
                var command = _statementRequestMapper.MapFrom(request);
                var transactions = await _mediator.SendAsync(command);
                foreach (var transaction in transactions)
                {
                    result.Add(_statementAPIMapper.MapTo(transaction));
                }

                if (result == null)
                {
                    return NotFound(new { Message = "Account not found." });
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

        [HttpGet("{id}/balance")]
        public async Task<IActionResult> GetBalance([FromRoute] int id)
        {
            try
            {
                var request = new BalanceRequest
                {
                    Id = id
                };
                var command = _balanceRequestMapper.MapFrom(request);
                var balance = await _mediator.SendAsync(command);

                if (balance == null)
                {
                    return NotFound(new { Message = "Account not found." });
                }

                return Ok(balance);
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

        [HttpPost("{id}/withdraw")]
        public async Task<IActionResult> Withdraw([FromRoute] int id, [Required] float amount)
        {
            try
            {
                var request = new WithdrawRequest
                {
                    Id = id,
                    Amount = amount
                };
                var command = _withdrawRequestMapper.MapFrom(request);
                var account = await _mediator.SendAsync(command);
                var result = _accountAPIMapper.MapTo(account);

                if (result == null)
                {
                    return NotFound(new { Message = "Account not found." });
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }
    }
}
