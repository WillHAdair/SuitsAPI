using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using suitsAPI.Authentication;
using suitsAPI.ClientUpdates;
using suitsAPI.Models;

namespace suitsAPI.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly IHubContext<UserHub> _hubContext;

        public DemoController(IHubContext<UserHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost(Name = "StartDemo")]
        [ServiceFilter(typeof(ApiKeyAuthFilter))]
        public async void StartDemo()
        {
            // Allow people to access the update methods
            ApiKeyValidation.RequestedRights[$"GET{HttpContext.Request.Path}"] = false;
            await _hubContext.Clients.All.SendAsync("StartDemo");
        }

        [HttpGet(Name = "GetUserCount")]
        [ServiceFilter(typeof(ApiKeyAuthFilter))]
        public async Task<Dictionary<string, bool>> GetUserCount(string demoType)
        {
            List<User> users = UserBase.GetUserByDemoType(UserBase.GetDemoType(demoType));
            Dictionary<string, bool> userStatusPairs = new Dictionary<string, bool>();
            foreach (User user in users)
            {
                userStatusPairs.Add(user.Username, user.IsReady);
            }

            // Send the user count to all connected clients
            await _hubContext.Clients.All.SendAsync("ReceiveUserCount", userStatusPairs);

            return userStatusPairs;
        }

        [HttpPost(Name = "RegisterUser")]
        [ServiceFilter(typeof(ApiKeyAuthFilter))]
        public void RegisterUser(string apiKey, string userName, string demoType)
        {
            // Register the user
            UserBase.AddUser(apiKey, userName, apiKey.Contains("/0000"), demoType);
        }
    }
}
