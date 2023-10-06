using KataBankAccount.Commands;
using KataBankAccount.Models;
using KataBankAccount.Queries;
using MediatR;
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

        [HttpGet("{Id}")]
        public async Task<BankAccount> GetBankAccountByIdAsync(int Id)
        {
            var bankAccountDetails = await _mediator.Send(new GetBankAccountByIdQuery() { Id = Id });

            return bankAccountDetails;
        }

        [HttpGet("{Id}/TransactionsHistory")]
        public async Task<string> GetTransactionAsync([FromRoute] int Id)
        {
            var TransactionDetails = await _mediator.Send(new GetHistoryQuery() { Id = Id });
            return TransactionDetails;
        }

        [HttpGet("{Id}/Balance")]
        public async Task<float> GetbalanceAsync([FromRoute] int Id)
        {
            var bankAccountDetails = await _mediator.Send(new GetBalanceQuery() { Id = Id });
            return bankAccountDetails;
        }

        [HttpPut("{Id}/Deposite")]
        public async Task<BankAccount> DepositeAsync([FromRoute] int Id, float amoutToAdd)
        {
            var bankAccountDetails = await _mediator.Send(new DepositeCommand(Id, amoutToAdd));
            return bankAccountDetails;
        }

        [HttpPut("{Id}/Withdraw")]
        public async Task<IActionResult> WithdrawAsync([FromRoute] int Id, float amountToSubstract)
        {
            var bankAccountDetails = await _mediator.Send(new WithdrawCommand(Id, amountToSubstract));
            if (bankAccountDetails == null)
            {
                return StatusCode(423, "Le solde du compte est insuffissant pour pour effectuer le retrait.");
            }
            return Ok(bankAccountDetails);
        }       

    }
}
