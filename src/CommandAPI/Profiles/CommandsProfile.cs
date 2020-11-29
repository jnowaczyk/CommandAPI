using AutoMapper;
using CommandAPI.Models;
using CommandAPI.Dtos;
namespace CommandAPI.Profiles
{
    class CommandsProfile: Profile
    {
        public CommandsProfile()
        {
            CreateMap<Command, CommandReadDto>();
            CreateMap<CommandCreateDto,Command>();
            CreateMap<CommandUpdateDto,Command>();
        }    
    }
}