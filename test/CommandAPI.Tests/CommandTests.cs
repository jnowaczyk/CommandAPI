using System;
using CommandAPI.Models;
using Xunit;

namespace CommandAPI.Tests
{
    public class CommandTests : IDisposable
    {
        Command testCommand;
        
        public CommandTests()
        {
            testCommand = new Command
            {
                HowTo = "Do something awesome",
                Platform = "xUnit",
                CommandLine = "dotnet test"
            };
        }


        [Fact]
        public void CanChangeHowTo() 
        {
            //Act
            testCommand.HowTo = "Execute Unit Tests";
            //Assert
            Assert.Equal("Execute Unit Tests", testCommand.HowTo);
        }

         [Fact]
        public void CanChangePlatform() 
        {
            //Act
            testCommand.Platform = "nUnit";
            //Assert
            Assert.Equal("nUnit", testCommand.Platform);
        }

         [Fact]
        public void CanChangeCommandLine() 
        {
            //Act
            testCommand.CommandLine = "Use runner instead";
            //Assert
            Assert.Equal("Use runner instead", testCommand.CommandLine);
        }

        public void Dispose() => testCommand = null;
    }
}