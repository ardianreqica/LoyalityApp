using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoyalityApp.Models
{
    public class LoyalityPointsTransaction
    {
        public Guid Id { get; set; }
        public Guid CusomerId { get; set; }
        public int Ammount { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
    }
}
