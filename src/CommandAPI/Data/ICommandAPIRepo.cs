using CommandAPI.Models;

namespace CommandAPI.Data;

public interface ICommandAPIRepo
{
    bool SaveChange();

    IEnumerable<Command> GetAllCommands();

    Command GetCommandById(int id);

    void CreateCommand(Command command);

    void UpdateCommand(Command command);

    void DeleteCommand(Command command);
}