using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecurrentJobsUsingHangfire
{
    public class MonitoringHub : Hub
    {
        public async Task SendToOther(string message)
        {
            await Clients.Others.SendAsync("ReceiveMessage", message);
        }
    }
}
