﻿using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class DbContextBank : DbContext
    {
        public DbContextBank() : base() { }
        public DbContextBank(DbContextOptions<DbContextBank> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<TransactionHistory> TransactionHistories { get; set; }
    }
}