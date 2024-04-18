using Microsoft.AspNetCore.SignalR;
namespace API.Hubs
{
    public class UserHub : Hub
    {
        public async Task SendUsers(Dictionary<string, bool> userStatusPairs)
        {
            await Clients.All.SendAsync("ReceiveUserCount", userStatusPairs);
        }

        public async Task StartDemo()
        {
            await Clients.All.SendAsync("DemoStarted");
        }
    }
}
