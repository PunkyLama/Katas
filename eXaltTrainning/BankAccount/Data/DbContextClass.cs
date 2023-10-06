using KataBankAccount.Models;
using Microsoft.EntityFrameworkCore;

namespace KataBankAccount.Data
{
    public class DbContextClass : DbContext
    {
        public DbContextClass() : base() { }
        public DbContextClass(DbContextOptions<DbContextClass> options) : base(options) { }
        public DbSet<BankAccount> BankAccounts { get; set; }
        //public DbSet<Transaction> Transactions { get; set; }
    }
}
