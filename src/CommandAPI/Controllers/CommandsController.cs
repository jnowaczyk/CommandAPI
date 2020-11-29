using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CommandAPI.Data;
using CommandAPI.Models;
using AutoMapper;
using CommandAPI.Dtos;

namespace CommandAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandAPIRepo _repository; 
        private readonly IMapper _mapper;

        public CommandsController(ICommandAPIRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands() 
        {
            var commands = _repository.GetAllCommands();

            var commandReadDtos = _mapper.Map<IEnumerable<CommandReadDto>>(commands);

            return Ok(commandReadDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<CommandReadDto> GetCommandByid(int id)
        {
            var commandEntity = _repository.GetCommandById(id);

            if (commandEntity == null)
            {
                return NotFound();
            }

            var readDto = _mapper.Map<CommandReadDto>(commandEntity);

            return Ok(readDto);
        }
    }
}