using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CommandAPI.Models;

namespace CommandAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly CommandContext _context;
        public CommandsController(CommandContext context) => _context = context;

        //GET:      api/commands
        [HttpGet]
        public IEnumerable<Command> GetCommandItems()
        { 
            return _context.CommandItems; 
        }
    }
}