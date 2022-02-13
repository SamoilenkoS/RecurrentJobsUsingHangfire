using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecurrentJobsUsingHangfire
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private Random _random;
        private IHubContext<MonitoringHub> _monitorHub;
        private Timer _timer = null!;

        public TimedHostedService(IHubContext<MonitoringHub> monitorHub)
        {
            _random = new Random();
            _monitorHub = monitorHub;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        private async void DoWork(object? state)
        {
            await _monitorHub.Clients.All.SendAsync("ReceiveMessage", _random.Next(-30, 30).ToString());
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
