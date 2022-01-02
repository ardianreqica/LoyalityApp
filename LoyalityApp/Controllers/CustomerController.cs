using LoyalityApp.Models;
using Microsoft.AspNetCore.Http;
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
    public class CustomerController : Controller
    {
        private LoyalityContext _context { get; set; }

        public CustomerController(
            LoyalityContext loyalityContext
            )
        {
            _context = loyalityContext;
        }

        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return _context.Customers;
        }

        [HttpGet]
        [Route("{id}")]
        public Customer Get(Guid id)
        {
            return _context.Customers.Where(t => t.Id == id)
               .Include(t => t.LoyalityPointsTransactions)
               .Include(t => t.Transactions)
               .Include(t=>t.CustomerLoyalityPoints)
               .FirstOrDefault();
        }

        [HttpGet]
        [Route("{id}/loyality-points")]
        public double GetLoyalityPoints(Guid id)
        {
            var loyalityPointInEuro = (_context.Customers.Where(t => t.Id == id)
               .Include(t => t.CustomerLoyalityPoints)
               .FirstOrDefault().CustomerLoyalityPoints.LoyalityPoint) / 100;

            return Math.Round((double)loyalityPointInEuro, 2); 
        }
    }
}
