using Domain.Models;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class DbContextBank : DbContext
    {
        public DbContextBank() : base() { }
        public DbContextBank(DbContextOptions<DbContextBank> options) : base(options) { }

        public DbSet<AccountEntity> Accounts { get; set; }
        public DbSet<TransactionHistoryEntity> TransactionHistories { get; set; }
    }
}