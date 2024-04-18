using suitsAPI.Models;
using Microsoft.AspNetCore.SignalR;
namespace suitsAPI.ClientUpdates
{
    public class UserHub : Hub
    {
        public async Task SendUsers(Dictionary<string, bool> userStatusPairs)
        {
            await Clients.All.SendAsync("UserStatus", userStatusPairs);
        }
    }
}
