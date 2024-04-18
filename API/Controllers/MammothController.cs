using API.Authentication;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MammothController : ControllerBase
    {

        private readonly ILogger<MammothController> _logger;

        public MammothController(ILogger<MammothController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetUpdate")]
        [ServiceFilter(typeof(ApiKeyAuthFilter))]
        public Mammoth GetUpdate(int instructionID)
        {
            List<Mammoth> updates = [
                new Mammoth(0, 1.45, "Move into position"),
                new Mammoth(1, 3.75, "Step off the platform"),
                new Mammoth(2, 9.5, "Walk a counter-clockwise circle")
            ];

            return updates[instructionID];
        }
    }
}
