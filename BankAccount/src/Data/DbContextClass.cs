using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DbContextClass : DbContext
    {
        public DbContextClass() : base() { }
        public DbContextClass(DbContextOptions<DbContextClass> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TransactionHistory> TransactionHistories { get; set; }
    }
}