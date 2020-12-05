using System.Collections.Generic;
using AutoMapper;
using CommandAPI.Controllers;
using CommandAPI.Data;
using CommandAPI.Models;
using CommandAPI.Profiles;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CommandAPI.Tests
{
    public class CommandsControllerTests
    {
        [Fact]
        public void GetCommanditems_ReturnsZeroItems_WhenDbIsEmpty() 
        {
            var mockRepository = new Mock<ICommandAPIRepo>();

            mockRepository.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(0));
            
            var profiles = new CommandsProfile();
            var configuration = new MapperConfiguration( configuration => configuration.AddProfile(profiles));

            IMapper mapper = new Mapper(configuration);
            
            var controller = new CommandsController(mockRepository.Object,mapper);

            // act: 
            var result = controller.GetAllCommands();

            // assert: 
            Assert.IsType<OkObjectResult>(result.Result);

        }

        private IEnumerable<Command> GetCommands(int num)
        {
            var commandsList = new List<Command>();

            if (num > 0) {
                commandsList.Add(new Command() 
                {
                    Id = 0,
                    HowTo = "How to generate a migration", 
                    CommandLine = "dotnet ef migrations add <name of migration>", 
                    Platform = ".NET EF Core"
                });
            }
            return commandsList;
        }
    }
}