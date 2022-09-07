using System.Linq;
using CommandAPI.Models;

namespace CommandAPI.Data;

public class SqlCommandAPIRepo : ICommandAPIRepo
{
    private readonly CommandContext _context;

    public SqlCommandAPIRepo(CommandContext context)
    {
        _context = context;
    }

    public void CreateCommand(Command command)
    {
        throw new NotImplementedException();
    }

    public void DeleteCommand(Command command)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Command> GetAllCommands() => _context.Commands.ToList();

    public Command GetCommandById(int id) => _context.Commands.FirstOrDefault(c => c.Id == id);

    public bool SaveChange()
    {
        throw new NotImplementedException();
    }

    public void UpdateCommand(Command command)
    {
        throw new NotImplementedException();
    }
}