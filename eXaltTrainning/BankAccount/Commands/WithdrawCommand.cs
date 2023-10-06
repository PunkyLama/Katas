﻿using KataBankAccount.Models;
using MediatR;

namespace KataBankAccount.Commands
{
    public class WithdrawCommand : IRequest<BankAccount>
    {
        public int Id { get; set; }
        public float amountToSubstract { get; set; }

        public WithdrawCommand(int id, float amountToSubstract)
        {
            this.Id = id;
            this.amountToSubstract = amountToSubstract;
        }
    }
}