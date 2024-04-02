using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using suitsAPI.Models;

namespace suitsAPI.Controllers
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

        private Factory factory;

        [HttpPost(Name = "StartDemo")]
        public void StartDemo()
        {
            // Make sure I am not declaring 
            factory = new Factory();
        }

        //[HttpGet(Name = "Initialize")]
        //public Mammoth Initialize()
        //{
        //    return new Mammoth();
        //}

        //[HttpGet(Name = "GetUpdate")]
        //public Mammoth GetUpdate(int instructionID)
        //{
        //    return null;
        //}
    }
}
