using KataBankAccount.Models;
using KataBankAccount.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace KataBankAccount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BankAccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("bankAccountId")]
        public async Task<BankAccount> GetBankAccountByIdAsync(int bankAccountId)
        {
            var bankAccountDetails = await _mediator.Send(new GetBankAccountByIdQuery() { Id = bankAccountId });

            return bankAccountDetails;
        }

    }
}
