using KataBankAccount.Commands;
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

        [HttpPost]
        public async Task<BankAccount> AddBankAccount(AddBankAccount bankAccount)
        {
            var bankAccountDetails = await _mediator.Send(new CreateBankAccountCommand(
                bankAccount.Name,
                bankAccount.Balance
                ));
            return bankAccountDetails;
        }

        [HttpGet("GetAccount/{Id}")]
        public async Task<BankAccount> GetBankAccountByIdAsync(int Id)
        {
            var bankAccountDetails = await _mediator.Send(new GetBankAccountByIdQuery() { Id = Id });

            return bankAccountDetails;
        }

        [HttpGet("GetTransactionHistory/{Id}")]
        public async Task<string> GetTransactionAsync([FromRoute] int Id)
        {
            var TransactionDetails = await _mediator.Send(new GetHistoryQuery() { Id = Id });
            return TransactionDetails;
        }

        [HttpGet("GetBalance/{Id}")]
        public async Task<float> GetbalanceAsync([FromRoute] int Id)
        {
            var bankAccountDetails = await _mediator.Send(new GetBalanceQuery() { Id = Id });
            return bankAccountDetails;
        }

        [HttpPut("Deposite/{Id}")]
        public async Task<BankAccount> DepositeAsync([FromRoute] int Id, float amoutToAdd)
        {
            var bankAccountDetails = await _mediator.Send(new DepositeCommand(Id, amoutToAdd));
            return bankAccountDetails;
        }

        [HttpPut("Withdraw/{Id}")]
        public async Task<BankAccount> WithdrawAsync([FromRoute] int Id, float amountToSubstract)
        {
            var bankAccountDetails = await _mediator.Send(new WithdrawCommand(Id, amountToSubstract));
            return bankAccountDetails;
        }       

    }
}
