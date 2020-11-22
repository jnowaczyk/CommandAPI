using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CommandAPI.Data;
using CommandAPI.Models;

namespace CommandAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandAPIRepo _repository; 

        public CommandsController(ICommandAPIRepo repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetAllCommands() 
        {
            var commands = _repository.GetAllCommands();
            return Ok(commands);
        }

        [HttpGet("{id}")]
        public ActionResult<Command> GetCommandByid(int id)
        {
            var commandEntity = _repository.GetCommandById(id);

            if (commandEntity == null)
            {
                return NotFound();
            }

            return Ok(commandEntity);
        }
    }
}