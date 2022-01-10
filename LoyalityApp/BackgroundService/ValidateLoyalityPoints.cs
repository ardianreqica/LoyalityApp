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
            // todo: schedule call for each customer every monday


            var count = Interlocked.Increment(ref executionCount);

            _logger.LogInformation(
                "Timed Hosted Service is working. Count: {Count}", count);
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
