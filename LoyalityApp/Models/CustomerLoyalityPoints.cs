using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoyalityApp.Models
{
    public class CustomerLoyalityPoints
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public int LoyalityPoint { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
    }
}
