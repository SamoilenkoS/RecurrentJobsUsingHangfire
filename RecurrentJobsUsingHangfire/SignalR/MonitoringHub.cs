using Microsoft.AspNetCore.SignalR;
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
