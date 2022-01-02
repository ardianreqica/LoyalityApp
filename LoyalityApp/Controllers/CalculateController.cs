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
    public class CalculateController : Controller
    {
        private LoyalityContext _context { get; set; }

        public CalculateController(
            LoyalityContext loyalityContext
            )
        {
            _context = loyalityContext;
        }


        [HttpGet]
        [Route("{customerId}")]
        public int Get(Guid customerId)
        {
            DayOfWeek weekStart = DayOfWeek.Monday; // or Sunday, or whenever
            DateTime startingDate = DateTime.Today;
            while (startingDate.DayOfWeek != weekStart)
                startingDate = startingDate.AddDays(-1);
            DateTime previousWeekStart = startingDate.AddDays(-7);
            DateTime previousWeekEnd = startingDate.AddDays(-1);


            foreach (var customer in _context.Customers
                .Include(lp => lp.LoyalityPointsTransactions)
                .Include(t => t.Transactions)
                )
            {
                #region  Check if the day is sunday and sum all amounts of transactions in that week

                var transactionAmmount = customer.Transactions
                    .Where(t => t.Date < previousWeekStart &
                            t.Date > previousWeekEnd &
                            t.CustomerId == customer.Id
                            )
                    .Sum(t => t.Ammount);

                #endregion


                #region At least one transaction exists for that customer on every day of the week

                var distinctTransactions = customer.Transactions
                    .Where(t => t.Date < previousWeekStart &
                            t.Date > previousWeekEnd &
                            t.CustomerId == customer.Id
                            ).GroupBy(t => t.Date.ToShortDateString());

                #endregion

                if (distinctTransactions.Count() > 7 & transactionAmmount > 500)
                {
                    customer.CustomerLoyalityPoints.LoyalityPoint += (int)transactionAmmount * 100;
                }
                else
                {
                    #region A user will lose all the points if no transaction was made in the last 5 weeks.

                    DateTime lastFiveWeekStart = startingDate.AddDays(-28);

                    var countTransactionsInFiveWeeks = customer.Transactions
                        .Where(t => t.Date < lastFiveWeekStart &
                                t.Date > previousWeekEnd);

                    if (countTransactionsInFiveWeeks.Count() == 0)
                        customer.CustomerLoyalityPoints.LoyalityPoint = 0;

                    #endregion
                }



            }

            return 0;

        }

    }
}




