using System;
using System.Collections.Generic;
using AutoMapper;
using CommandAPI.Controllers;
using CommandAPI.Data;
using CommandAPI.Dtos;
using CommandAPI.Models;
using CommandAPI.Profiles;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CommandAPI.Tests
{
    public class CommandsControllerTests : IDisposable
    {
        private Mock<ICommandAPIRepo> _apiRepoMock;
        private IMapper _mapper;
        public CommandsControllerTests()
        {
            _apiRepoMock = new Mock<ICommandAPIRepo>();
            _mapper = InitializeMapper();
        }
        
        private IMapper InitializeMapper() 
        {
            var profile = new CommandsProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            return new Mapper(configuration);
        }


        
        
        [Fact]
        public void GetCommanditems_ReturnsZeroItems_WhenDbIsEmpty() 
        {
            _apiRepoMock.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(0));
            var controller = new CommandsController(_apiRepoMock.Object,_mapper);

            // act: 
            var result = controller.GetAllCommands();

            // assert: 
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAllCommands_ReturnsSingleCommand_WhenResourceHasSingleCommand() 
        {
            _apiRepoMock.Setup( repo => repo.GetAllCommands()).Returns(GetCommands(1));

            var controller = new CommandsController(_apiRepoMock.Object, _mapper);

            var actual = controller.GetAllCommands();

            var result = actual.Result as OkObjectResult;

            var commands = result.Value as List<CommandReadDto>;

            Assert.Single(commands);
        }


         [Fact]
        public void GetAllCommands_ReturnsPropperType_WhenResourceHasSingleCommand() 
        {
            _apiRepoMock.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));
            var controller = new CommandsController(_apiRepoMock.Object,_mapper);

            var actual = controller.GetAllCommands();
            actual.Result.GetType();
            Assert.IsType<ActionResult<IEnumerable<CommandReadDto>>>(actual);
        }

        [Fact]
        public void GetCommandById_ReturnsNoFound_WhenCommandDoesNotExists() 
        {
            _apiRepoMock.Setup(repo => repo.GetCommandById(0)).Returns(() => null);
            var controller = new CommandsController(_apiRepoMock.Object,_mapper);

            var actual = controller.GetCommandById(3);

            Assert.IsType<NotFoundResult>(actual.Result);
        }

         [Fact]
        public void GetCommandById_ReturnsOk_WhenCommandExists() 
        {
            _apiRepoMock.Setup(repo => repo.GetCommandById(1)).Returns(new Command() { Id = 1, CommandLine = "mockCommand", HowTo = "mockHowTo", Platform = "mockPlatform"});
            var controller = new CommandsController(_apiRepoMock.Object,_mapper);

            var actual = controller.GetCommandById(1);

            Assert.IsType<OkObjectResult>(actual.Result);
        }

        [Fact]
        public void GetCommandById_ReturnsCommandReadDto_WhenCommandExists() 
        {
            _apiRepoMock.Setup(repo => repo.GetCommandById(2)).Returns(new Command() { Id = 2, CommandLine = "mockCommand", HowTo = "mockHowTo", Platform = "mockPlatform"});
            var controller = new CommandsController(_apiRepoMock.Object,_mapper);

            var actual = controller.GetCommandById(2);

            Assert.IsType<ActionResult<CommandReadDto>>(actual);
        }


        [Fact]
        public void CreateCommand_ReturnsCreatedStatus_WhenValidObjectSubmitted() 
        {
            _apiRepoMock.Setup(repo => repo.GetCommandById(1)).Returns(new Command() 
            {
                Id = 1,
                HowTo = "mock",
                Platform = "Mock",
                CommandLine = "Mock" 
            });

            var controller = new CommandsController(_apiRepoMock.Object, _mapper);

            var actual = controller.CreateCommand(new CommandCreateDto());

            Assert.IsType<ActionResult<CommandReadDto>>(actual);
        }

        [Fact]
        public void CreateCommand_Returns201CreatedResult_WhenValidObjectSubmitted() 
        {
            _apiRepoMock.Setup(repo => repo.GetCommandById(1)).Returns(new Command() 
            {
                Id = 1,
                HowTo = "mock",
                Platform = "Mock",
                CommandLine = "Mock" 
            });

            var controller = new CommandsController(_apiRepoMock.Object, _mapper);

            var actual = controller.CreateCommand(new CommandCreateDto());

            Assert.IsType<CreatedAtRouteResult>(actual.Result);
        }

        [Fact]
        public void CreateCommand_Returns204NoContent_WhenValidObjectSubmitted() 
        {
            _apiRepoMock.Setup(repo => repo.GetCommandById(1)).Returns(new Command() 
            {
                Id = 1,
                HowTo = "mock",
                Platform = "Mock",
                CommandLine = "Mock" 
            });

            var controller = new CommandsController(_apiRepoMock.Object, _mapper);

            var actual = controller.UpdateCommand(1, new CommandUpdateDto());

            Assert.IsType<NoContentResult>(actual);
        }

        [Fact]
        public void CreateCommand_Returns404NotFount_WhenNotExistingObjectSubmitted() 
        {
            _apiRepoMock.Setup(repo => repo.GetCommandById(1)).Returns(new Command() 
            {
                Id = 1,
                HowTo = "mock",
                Platform = "Mock",
                CommandLine = "Mock" 
            });

            var controller = new CommandsController(_apiRepoMock.Object, _mapper);

            var actual = controller.UpdateCommand(2, new CommandUpdateDto());

            Assert.IsType<NotFoundResult>(actual);
        }


         [Fact]
        public void CreateCommand_Returns404NotFount_WhenNoObjectsIdRepository() 
        {
            _apiRepoMock.Setup(repo => repo.GetCommandById(1)).Returns(() => null);

            var controller = new CommandsController(_apiRepoMock.Object, _mapper);

            var actual = controller.UpdateCommand(1, new CommandUpdateDto());

            Assert.IsType<NotFoundResult>(actual);
        }

        #region [Partial update]

        [Fact]
        public void PartialCommandUpdate_Returns404_WhenResourceNotExistsIdSubmitted() 
        {
            _apiRepoMock.Setup(repo => repo.GetCommandById(0)).Returns(() => null);
            var controller = new CommandsController(_apiRepoMock.Object,_mapper);

            var actual = controller.PartialUpdateCommand(2, new JsonPatchDocument<CommandUpdateDto>());

            Assert.IsType<NotFoundResult>(actual);
        }

        [Fact]
        public void DeleteCommand_Returns204NoContent_WhenResourceExistsIdSubmitted() 
        {
            _apiRepoMock.Setup(repo => repo.GetCommandById(1)).Returns(new Command() 
            {
                Id = 1,
                HowTo = "mock",
                Platform = "Mock",
                CommandLine = "Mock" 
            });
            var controller = new CommandsController(_apiRepoMock.Object,_mapper);

            var actual = controller.DeleteCommand(1);

            Assert.IsType<NoContentResult>(actual);
        }

         [Fact]
        public void DeleteCommand_Returns204NoContent_WhenResourceNotExistsIdSubmitted() 
        {
            _apiRepoMock.Setup(repo => repo.GetCommandById(1)).Returns(new Command() 
            {
                Id = 1,
                HowTo = "mock",
                Platform = "Mock",
                CommandLine = "Mock" 
            });
            var controller = new CommandsController(_apiRepoMock.Object,_mapper);

            var actual = controller.DeleteCommand(2);

            Assert.IsType<NotFoundResult>(actual);
        }


        #endregion


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

        public void Dispose()
        {
            _apiRepoMock = null;
            _mapper = null;
        }
    }
}