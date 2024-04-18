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
        public string StartDemo()
        {
            // Allow people to access the update methods
            ApiKeyValidation.RequestedRights[$"GET{HttpContext.Request.Path}"] = false;
            return "Demo Started";
        }

        [HttpGet(Name = "GetUserCount")]
        [ServiceFilter(typeof(ApiKeyAuthFilter))]
        public async Task<Dictionary<string, bool>> GetUserCount(string demoType)
        {
            List<User> users = GetUserByDemoType(GetDemoType(demoType));
            int userCount = users.Count;

            // Send the user count to all connected clients
            await _hubContext.Clients.All.SendAsync("ReceiveUserCount", userCount);

            return users;
        }

        [HttpPost(Name = "RegisterUser")]
        [ServiceFilter(typeof(ApiKeyAuthFilter))]
        public void RegisterUser(string apiKey, string userName, string demoType)
        {
            // Register the user
            RegisterUserUsingAPIKeys(user);
        }
    }
}
