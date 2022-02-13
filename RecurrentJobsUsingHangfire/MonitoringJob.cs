using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Threading.Tasks;

namespace RecurrentJobsUsingHangfire
{
    public class MonitoringJob
    {
        private static int _count;
        private EFCoreContext _dbContext;
        private IHubContext<MonitoringHub> _monitorHub;
        private ILogger<MonitoringJob> _logger;

        public MonitoringJob(
            EFCoreContext dbContext,
            IHubContext<MonitoringHub> monitorHub,
            ILogger<MonitoringJob> logger)
        {
            _dbContext = dbContext;
            _monitorHub = monitorHub;
            _logger = logger;
        }

        public async Task StartJob()
        {
            _logger.LogInformation("Current count: {count}", ++_count);

            var items = await _dbContext.Goods.ToListAsync();
            await _monitorHub.Clients.All.SendAsync(
                "ReceiveMessage",
                JsonSerializer.Serialize(items));
        }
    }
}
