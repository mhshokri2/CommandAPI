using System;
using Xunit;
using CommandAPI.Models;

namespace CommandAPI.Tests;

public class CommandTests : IDisposable
{
    private Command command;

    public CommandTests()
    {
        command = new()
        {
            HowTo = "Do something awesome",
            Platform = "xUnit",
            CommandLine = "dotnet test"
        };
    }

    public void Dispose()
    {
        command = null;
    }

    [Fact]
    public void CanChangeHowTo()
    {
        //Arrange

        //Act
        command.HowTo = "Execute Unit Tests";

        //Assert
        Assert.Equal("Execute Unit Tests", command.HowTo);
    }

    [Fact]
    public void CanChangePlatform()
    {
        //Arrange

        //Act
        command.Platform = "Execute Unit Tests";

        //Assert
        Assert.Equal("Execute Unit Tests", command.Platform);
    }

    [Fact]
    public void CanChangeCommandLine()
    {
        //Arrange

        //Act
        command.CommandLine = "Execute Unit Tests";

        //Assert
        Assert.Equal("Execute Unit Tests", command.CommandLine);
    }
}
