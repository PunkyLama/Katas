using AutoMapper;
using Domain.Commands;
using Domain.Injection;
using Domain.Models;
using Domain.Queries;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Web.API.Models.Dto;
using Web.API.Models.Responses;

namespace Web.API
{
    [Route("Accounts")]
    [ApiController]
    public class Controller : ControllerBase
    {
        private readonly IMediatr _mediator;
        private readonly IMapper _mapper;

        public Controller(IMediatr mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("{id}/deposit")]
        public async Task<IActionResult> Deposit([FromRoute] int id, [Required] float amount)
        {
            try
            {
                var request = new DepositDto
                {
                    Id = id,
                    Amount = amount
                };
                var command = _mapper.Map<DepositCommand>(request);
                var account = await _mediator.SendAsync(command);
                var result = _mapper.Map<AccountResponse>(account);

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
                var request = new StatementDto
                {
                    Id = id,
                    Element = element
                };
                var command = _mapper.Map<StatementQuery>(request);
                var transactions = await _mediator.SendAsync(command);
                foreach (var transaction in transactions)
                {
                    result.Add(_mapper.Map<StatementReponse>(transaction));
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
                var request = new BalanceDto
                {
                    Id = id
                };
                var command = _mapper.Map<BalanceQuery>(request);
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
                var request = new WithdrawDto
                {
                    Id = id,
                    Amount = amount
                };
                var command = _mapper.Map<WithdrawCommand>(request);
                var account = await _mediator.SendAsync(command);
                var result = _mapper.Map<AccountResponse>(account);

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
