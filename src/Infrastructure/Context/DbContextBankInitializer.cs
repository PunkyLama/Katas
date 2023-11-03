using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class DbContextBankInitializer
    {
        public static void InitializeData(DbContextBank dbContextBank) 
        {
            dbContextBank.Database.EnsureCreated();

            if (dbContextBank.Accounts.Any() )
            {
                return;
            }

            var accounts = new List<AccountEntity>
            {
                new AccountEntity {Id = 1, Balance = 100},
                new AccountEntity {Id = 2, Balance = 500}
            };

            dbContextBank.Accounts.AddRange(accounts);
            dbContextBank.SaveChanges();        
        }
    }
}
