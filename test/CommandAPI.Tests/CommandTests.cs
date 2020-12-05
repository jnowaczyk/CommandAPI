using CommandAPI.Models;
using Xunit;

namespace CommandAPI.Tests
{
    public class CommandTests 
    {
        [Fact]
        public void CanChangeHowTo() 
        {
            var testCommand = new Command
            {
                HowTo = "Do something awesome",
                Platform = "xUnit",
                CommandLine = "dotnet test"
            };
            //Act
            testCommand.HowTo = "Execute Unit Tests";
            //Assert
            Assert.Equal("Execute Unit Tests", testCommand.HowTo);
        }

         [Fact]
        public void CanChangePlatform() 
        {
            var testCommand = new Command
            {
                HowTo = "Do something awesome",
                Platform = "xUnit",
                CommandLine = "dotnet test"
            };
            //Act
            testCommand.Platform = "nUnit";
            //Assert
            Assert.Equal("nUnit", testCommand.Platform);
        }

         [Fact]
        public void CanChangeCommandLine() 
        {
            var testCommand = new Command
            {
                HowTo = "Do something awesome",
                Platform = "xUnit",
                CommandLine = "dotnet test"
            };
            //Act
            testCommand.CommandLine = "Use runner instead";
            //Assert
            Assert.Equal("Use runner instead", testCommand.CommandLine);
        }
    }
}