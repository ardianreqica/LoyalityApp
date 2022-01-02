using LoyalityApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyalityApp
{
    public class LoyalityContext : DbContext
    {
        public LoyalityContext(DbContextOptions<LoyalityContext> options)
            : base(options)
        { }
        
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerLoyalityPoints> CustomerLoyalityPoints { get; set; }
        public DbSet<LoyalityPointsTransaction> LoyalityPointsTransactions { get; set; }


        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionPointRange> TransactionPointRanges { get; set; }

    }
}
