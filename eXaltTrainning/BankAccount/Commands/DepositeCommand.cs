﻿using KataBankAccount.Models;
using MediatR;

namespace KataBankAccount.Commands
{
    public class DepositeCommand : IRequest<BankAccount>
    {
        public int Id { get; set; }
        public float amountToAdd { get; set; }

        public DepositeCommand(int id, float amountToAdd)
        {
            this.Id = id;
            this.amountToAdd = amountToAdd;
        }
    }
}