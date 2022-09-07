using AutoMapper;
using CommandAPI.Data;
using CommandAPI.Dtos;
using CommandAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommandsController : ControllerBase
{
    private readonly ICommandAPIRepo _commandAPIRepo;
    private readonly IMapper _mapper;
    public CommandsController(ICommandAPIRepo commandAPIRepo, IMapper mapper)
    {
        _commandAPIRepo = commandAPIRepo;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
    {
        IEnumerable<Command> commands = _commandAPIRepo.GetAllCommands();
        IEnumerable<CommandReadDto> dtos = _mapper.Map<IEnumerable<CommandReadDto>>(commands);
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public ActionResult<CommandReadDto> GetCommandById(int id)
    {
        Command command = _commandAPIRepo.GetCommandById(id);
        CommandReadDto dto = _mapper.Map<CommandReadDto>(command);
        return Ok(dto);
    }
}