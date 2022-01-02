using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoyalityApp.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public decimal Ammount { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
    }
}
