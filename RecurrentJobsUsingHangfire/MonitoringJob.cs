using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace RecurrentJobsUsingHangfire
{
    public class MonitoringJob
    {
        private const int TotalJobDuration = 55;
        private const int DelayBetweenMessages = 5;
        private static int _count;
        private EFCoreContext _dbContext;
        private IHubContext<MonitoringHub> _monitorHub;
        private ILogger<MonitoringJob> _logger;
        private DateTime _startDateTime;

        public MonitoringJob(
            EFCoreContext dbContext,
            IHubContext<MonitoringHub> monitorHub,
            ILogger<MonitoringJob> logger)
        {
            _dbContext = dbContext;
            _monitorHub = monitorHub;
            _logger = logger;
            _startDateTime = DateTime.Now;
        }

        public async Task StartJob()
        {
            _logger.LogInformation("Job run started!");
            while(DateTime.Now.Subtract(_startDateTime).TotalSeconds < TotalJobDuration)
            {
                await JobBody();

                await Task.Delay(TimeSpan.FromSeconds(DelayBetweenMessages));
            }

            _logger.LogInformation("Job run finished!");
        }

        private async Task JobBody()
        {
            _logger.LogInformation("Current count: {count}", ++_count);

            var items = await _dbContext.Goods.ToListAsync();
            await _monitorHub.Clients.All.SendAsync(
                "ReceiveMessage",
                JsonSerializer.Serialize(items));
        }
    }
}
