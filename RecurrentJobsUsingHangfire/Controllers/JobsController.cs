using Hangfire;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace RecurrentJobsUsingHangfire.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobsController : ControllerBase
    {
        private IServiceProvider _serviceProvider;

        public JobsController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [HttpPut("monitoring")]
        public async Task ManipulateJob(bool start)
        {
            if (start)
            {
                var job = (MonitoringJob)_serviceProvider.GetService(typeof(MonitoringJob));
                RecurringJob.AddOrUpdate(
                    nameof(MonitoringJob),
                    () => job.StartJob(),
                    Cron.Minutely);
            }
            else
            {
                RecurringJob.RemoveIfExists(nameof(MonitoringJob));
            }
        }
    }
}
