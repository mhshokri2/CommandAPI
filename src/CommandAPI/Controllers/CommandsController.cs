using System.Collections.Generic;
using CommandAPI.Data;
using CommandAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommandsController : ControllerBase
{
    private readonly ICommandAPIRepo _commandAPIRepo;
    public CommandsController(ICommandAPIRepo commandAPIRepo)
    {
        _commandAPIRepo = commandAPIRepo;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Command>> GetAllCommands() => Ok(_commandAPIRepo.GetAllCommands());

    [HttpGet("{id}")]
    public ActionResult<Command> GetCommandById(int id) => Ok(_commandAPIRepo.GetCommandById(id));
}