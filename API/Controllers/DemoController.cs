using API.Authentication;
using API.Hubs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using API.Models;

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
        public KeyValuePair<bool, string> RegisterUser(string apiKey, string userName, string demoType)
        {
            // Register the user
            var success = UserBase.AddUser(apiKey, userName, apiKey.Contains("/0000"), demoType);
            return success ? new KeyValuePair<bool, string> ( true, "User registered" ) : new KeyValuePair<bool, string> ( false, "User already registered" );
        }
    }
}