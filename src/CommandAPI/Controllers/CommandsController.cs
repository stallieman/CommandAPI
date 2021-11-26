using System.Collections;
using Microsoft.AspNetCore.Mvc;

namespace CommandAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable Get() => new string[] { "This", "is", "hard", "coded" };
    }
}