using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace SignalRConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            HubConnection connection
               = new HubConnectionBuilder()
               .WithUrl("https://localhost:5001/monitoring")
               .Build();
            connection.On<string>("ReceiveMessage",
                (message) =>
                {
                    Console.WriteLine(message);
                });

            await connection.StartAsync();

            while (true)
            {
                await connection.SendAsync("SendToOther", "Hello from client");
                Console.ReadKey();
            }
        }
    }
}
