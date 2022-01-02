using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LoyalityApp.BackgroundService
{
    //public class ValidateLoyalityPoints
    //{
    //}

    public class TimedHostedService : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private readonly ILogger<TimedHostedService> _logger;
        private Timer _timer = null!;
        private readonly LoyalityContext _context;


        public TimedHostedService(
            ILogger<TimedHostedService> logger,
            LoyalityContext context)
        {
            _logger = logger;
            _context = context;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        private void DoWork(object? state)
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


                #region A user will lose all the points if no transaction was made in the last 5 weeks.

                DateTime lastFiveWeekStart = startingDate.AddDays(-28);

                var countTransactionsInFiveWeeks = customer.Transactions
                    .Where(t => t.Date < lastFiveWeekStart &
                            t.Date > previousWeekEnd);

                if (countTransactionsInFiveWeeks.Count() == 0)
                    customer.CustomerLoyalityPoints.LoyalityPoint = 0;

                #endregion

            }





            var count = Interlocked.Increment(ref executionCount);



            _logger.LogInformation(
                "Timed Hosted Service is working. Count: {Count}", count);


            //           1) Check if the day is sunday and sum all amounts of transactions in that week
            //if the sum > 500 than loyaltyPoints += sum


            //foreach(var customer in )



        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }


        private int CalulateLoyalityPoint(int points)
        {
            decimal result = 0;
            var pointRange = _context.TransactionPointRanges;

            foreach (var range in pointRange)
            {
                if (points > range.Max)
                    result += (range.Max - range.Min) * range.PointValue;
                else
                    if (range.Min < points && points < range.Max)
                    result += (result - range.Min) * range.PointValue;
            }
            return (int)result;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }

}
