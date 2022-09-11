using System.Collections.Generic;
using Moq;
using AutoMapper;
using CommandAPI.Models;
using CommandAPI.Data;
using Xunit;
using CommandAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using CommandAPI.Profiles;
using CommandAPI.Dtos;
using System;

namespace CommandAPI.Tests;

public class CommandControllerTests : IDisposable
{
    Mock<ICommandAPIRepo> mockRepo;
    CommandProfile realProfile;
    MapperConfiguration configuration;
    IMapper mapper;

    public CommandControllerTests()
    {
        mockRepo = new();
        realProfile = new();
        configuration = new(cfg => cfg.AddProfile(realProfile));
        mapper = new Mapper(configuration);
    }

    [Fact]
    public void GetCommand_returnZero_WhenDBIsEmpty()
    {
        //Arrange
        mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(0));
        CommandsController controller = new(mockRepo.Object, mapper);

        //Act
        ActionResult<IEnumerable<CommandReadDto>> actionResult = controller.GetAllCommands();

        //Assert
        Assert.IsType<OkObjectResult>(actionResult.Result);

    }

    [Fact]
    public void GetAllCommands_ReturnsOneItem_WhenDBHasOneResource()
    {
        //Arrange
        mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));
        CommandsController controller = new(mockRepo.Object, mapper);

        //Act
        ActionResult<IEnumerable<CommandReadDto>> actionResult = controller.GetAllCommands();

        //Assert
        OkObjectResult okObjectResult = actionResult.Result as OkObjectResult;
        List<CommandReadDto> command = okObjectResult.Value as List<CommandReadDto>;

        Assert.Single(command);
    }


    [Fact]
    public void GetAllCommands_Returns200OK_WhenDBHasOneResource()
    {
        //Arrange
        mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));
        CommandsController controller = new(mockRepo.Object, mapper);

        //Act
        ActionResult<IEnumerable<CommandReadDto>> result = controller.GetAllCommands();

        //Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public void GetAllCommands_ReturnsCorrectType_WhenDBHasOneResource()
    {
        //Arrange
        mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));
        CommandsController controller = new(mockRepo.Object, mapper);

        //Act
        ActionResult<IEnumerable<CommandReadDto>> result = controller.GetAllCommands();

        //Assert
        Assert.IsType<ActionResult<IEnumerable<CommandReadDto>>>(result);
    }
    [Fact]
    public void GetCommandByID_Returns404NotFound_WhenNonExistentIDProvided()
    {
        //Arrange
        mockRepo.Setup(repo => repo.GetCommandById(0)).Returns(() => null);
        CommandsController controller = new(mockRepo.Object, mapper);

        //Act
        ActionResult<CommandReadDto> result = controller.GetCommandById(1);

        //Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public void GetCommandByID_Returns200OK__WhenValidIDProvided()
    {
        //Arrange
        mockRepo.Setup(repo => repo.GetCommandById(1)).Returns(new Command
        {
            Id = 1,
            HowTo = "mock",
            Platform = "Mock",
            CommandLine = "Mock"
        });
        CommandsController controller = new(mockRepo.Object, mapper);

        //Act
        ActionResult<CommandReadDto> result = controller.GetCommandById(1);

        //Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }


    [Fact]
    public void GetCommandByID_ReturnsCorrectObject__WhenValidIDProvided()
    {
        //Arrange
        mockRepo.Setup(repo => repo.GetCommandById(1)).Returns(new Command
        {
            Id = 1,
            HowTo = "mock",
            Platform = "Mock",
            CommandLine = "Mock"
        });
        CommandsController controller = new(mockRepo.Object, mapper);

        //Act
        ActionResult<CommandReadDto> result = controller.GetCommandById(1);

        //Assert
        Assert.IsType<ActionResult<CommandReadDto>>(result);
    }


    [Fact]
    public void CreateCommand_ReturnsCorrectResourceType_WhenValidObjectSubmitted()
    {
        //Arrange
        mockRepo.Setup(repo => repo.GetCommandById(1)).Returns(new Command
        {
            Id = 1,
            HowTo = "mock",
            Platform = "Mock",
            CommandLine = "Mock"
        });
        CommandsController controller = new(mockRepo.Object, mapper);

        //Act
        ActionResult<CommandReadDto> result = controller.CreateCommand(new CommandCreateDto { });

        //Assert
        Assert.IsType<ActionResult<CommandReadDto>>(result);
    }
    [Fact]
    public void CreateCommand_Returns201Created_WhenValidObjectSubmitted()
    {
        //Arrange
        mockRepo.Setup(repo => repo.GetCommandById(1)).Returns(new Command
        {
            Id = 1,
            HowTo = "mock",
            Platform = "Mock",
            CommandLine = "Mock"
        });
        CommandsController controller = new(mockRepo.Object, mapper);
        //Act
        ActionResult<CommandReadDto> result = controller.CreateCommand(new CommandCreateDto { });
        //Assert
        Assert.IsType<CreatedAtRouteResult>(result.Result);
    }

    [Fact]
    public void UpdateCommand_Returns204NoContent_WhenValidObjectSubmitted()
    {
        //Arrange
        mockRepo.Setup(repo => repo.GetCommandById(1)).Returns(new Command
        {
            Id = 1,
            HowTo = "mock",
            Platform = "Mock",
            CommandLine = "Mock"
        });
        CommandsController controller = new(mockRepo.Object, mapper);

        //Act
        ActionResult result = controller.UpdateCommand(1, new CommandUpdateDto { });

        //Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void UpdateCommand_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
    {
        //Arrange
        mockRepo.Setup(repo => repo.GetCommandById(0)).Returns(() => null);
        CommandsController controller = new(mockRepo.Object, mapper);

        //Act
        ActionResult result = controller.UpdateCommand(0, new CommandUpdateDto { });

        //Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void PartialCommandUpdate_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
    {
        //Arrange
        mockRepo.Setup(repo => repo.GetCommandById(0)).Returns(() => null);
        var controller = new CommandsController(mockRepo.Object, mapper);

        //Act
        IActionResult result = controller.PartialCommandUpdatead(0, new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<CommandUpdateDto> { });

        //Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void DeleteCommand_Returns204NoContent_WhenValidResourceIDSubmitted()
    {
        //Arrange
        mockRepo.Setup(repo => repo.GetCommandById(1)).Returns(new Command
        {
            Id = 1,
            HowTo = "mock",
            Platform = "Mock",
            CommandLine = "Mock"
        });
        CommandsController controller = new(mockRepo.Object, mapper);

        //Act
        IActionResult result = controller.DeleteCommand(1);

        //Assert
        Assert.IsType<NoContentResult>(result);
    }
    [Fact]
    public void DeleteCommand_Returns_404NotFound_WhenNonExistentResourceIDSubmitted()
    {
        //Arrange
        mockRepo.Setup(repo => repo.GetCommandById(0)).Returns(() => null);
        CommandsController controller = new(mockRepo.Object, mapper);

        //Act
        IActionResult result = controller.DeleteCommand(0);

        //Assert
        Assert.IsType<NotFoundResult>(result);
    }

    private List<Command> GetCommands(int index)
    {
        List<Command> resultValue = new();
        if (index > 0)
        {
            resultValue.Add(new()
            {
                Id = 0,
                HowTo = "How to generate a migration",
                CommandLine = "dotnet ef migrations add <Name of Migration>",
                Platform = ".Net Core EF"
            });
        }
        return resultValue;
    }

    public void Dispose()
    {
        mockRepo = null;
        realProfile = null;
        configuration = null;
        mapper = null;
    }
}