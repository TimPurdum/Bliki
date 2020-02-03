using Bliki.Data;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace Bliki
{
    public class SessionCleaner : IHostedService, IDisposable
    {
        public SessionCleaner(ILogger<SessionCleaner> logger,
            PageManager pageManager)
        {
            _logger = logger;
        }


        public void Dispose()
        {
            _timer?.Dispose();
        }


        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromMinutes(1));

            return Task.CompletedTask;
        }


        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }


        private void DoWork(object? state)
        {
            PageManager.ClearAbandonedEditingSessions();
        }


        private readonly ILogger<SessionCleaner> _logger;
        private Timer? _timer;
    }
}