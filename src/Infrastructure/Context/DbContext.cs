using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class DbContextBank : DbContext
    {
        public DbContextBank() : base() { }
        public DbContextBank(DbContextOptions<DbContextBank> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountEntity>()
                .HasMany(e => e.TransactionHistories) // AccountEntity has many TransactionHistory
                .WithOne(e => e.Account) // TransactionHistory has one AccountEntity
                .OnDelete(DeleteBehavior.Cascade); // Delete TransactionHistory when AccountEntity is deleted

            modelBuilder.Entity<AccountEntity>().HasData(
                new AccountEntity
                {
                    Id = 1,
                    Balance = 100,
                },
                new AccountEntity
                {
                    Id = 2,
                    Balance = 500,
                }
                );
        }

        public DbSet<AccountEntity> Accounts { get; set; }
        public DbSet<TransactionHistoryEntity> TransactionHistories { get; set; }
    }
}