using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyalityApp.Models
{
    public class TransactionPointRange
    {
        public Guid Id { get; set; }
        public decimal Min { get; set; }
        public decimal Max { get; set; }
        public decimal PointValue { get; set; }
    }
}
