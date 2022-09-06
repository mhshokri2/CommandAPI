using System.Collections.Generic;
using CommandAPI.Models;

namespace CommandAPI.Data;

public class MockCommandAPIRepo : ICommandAPIRepo
{
    public void CreateCommand(Command command)
    {
        throw new NotImplementedException();
    }

    public void DeleteCommand(Command command)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Command> GetAllCommands()
    {
        List<Command> commands = new()
        {
            new()
            {
                CommandLine = "dotnet ef migrations add <Name of Migration>",
                HowTo = "How to generate a migration",
                Id = 0,
                Platform = ".Net Core EF"
            },
            new()
            {
                CommandLine = "dotnet ef database update",
                HowTo = "Run Migrations",
                Id = 1,
                Platform = ".Net Core EF"
            },
            new()
            {
                CommandLine = "dotnet ef migrations list",
                HowTo = "List active migrations",
                Id = 2,
                Platform = ".Net Core EF"
            },
        };
        return commands;
    }

    public Command GetCommandById(int id) => new()
    {
        CommandLine = "dotnet ef migrations add <Name of Migration>",
        HowTo = "How to generate a migration",
        Id = 0,
        Platform = ".Net Core EF"
    };



    public bool SaveChange()
    {
        throw new NotImplementedException();
    }

    public void UpdateCommand(Command command)
    {
        throw new NotImplementedException();
    }
}