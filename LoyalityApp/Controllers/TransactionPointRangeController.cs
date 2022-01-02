using LoyalityApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyalityApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionPointRangeController : Controller
    {
        private LoyalityContext _context { get; set; }

        public TransactionPointRangeController(
            LoyalityContext loyalityContext
            )
        {
            _context = loyalityContext;
        }

        [HttpGet]
        public IEnumerable<TransactionPointRange> Get()
        {
            return _context.TransactionPointRanges.ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public TransactionPointRange Get(Guid id)
        {
            return _context.TransactionPointRanges
                .Where(t => t.Id == id)
                .FirstOrDefault();
        }
        


    }
}


