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
        if (command == null)
        {
            throw new ArgumentNullException(nameof(command));
        }
        _context.Commands.Add(command);
    }

    public void DeleteCommand(Command command)
    {
        if (command == null)
        {
            throw new ArgumentNullException();
        }
        _context.Remove(command);
    }

    public IEnumerable<Command> GetAllCommands() => _context.Commands.ToList();

    public Command GetCommandById(int id) => _context.Commands.FirstOrDefault(c => c.Id == id);

    public bool SaveChange() => _context.SaveChanges() >= 0;

    public void UpdateCommand(Command command)
    { }
}