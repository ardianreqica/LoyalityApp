using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoyalityApp.Models
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } 

        public CustomerLoyalityPoints CustomerLoyalityPoints { get; set; }

        public List<Transaction> Transactions { get; set; }
        public List<LoyalityPointsTransaction> LoyalityPointsTransactions { get; set; }

    }
}
