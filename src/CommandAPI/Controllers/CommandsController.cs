using AutoMapper;
using CommandAPI.Data;
using CommandAPI.Dtos;
using CommandAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
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

    [HttpGet("{id}", Name = "GetCommandById")]
    public ActionResult<CommandReadDto> GetCommandById(int id)
    {
        Command command = _commandAPIRepo.GetCommandById(id);
        if (command == null)
        {
            return NotFound();
        }
        CommandReadDto dto = _mapper.Map<CommandReadDto>(command);
        return Ok(dto);
    }

    [HttpPost]
    public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
    {
        Command command = _mapper.Map<Command>(commandCreateDto);
        _commandAPIRepo.CreateCommand(command);
        _commandAPIRepo.SaveChange();

        CommandReadDto commandReadDto = _mapper.Map<CommandReadDto>(command);

        return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDto.Id }, commandReadDto);
    }

    [HttpPut("{id}")]
    public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
    {
        Command command = _commandAPIRepo.GetCommandById(id);
        if (command == null)
        {
            return NotFound();
        }
        _mapper.Map(commandUpdateDto, command);
        _commandAPIRepo.UpdateCommand(command);
        _commandAPIRepo.SaveChange();

        return NoContent();
    }

    [HttpPatch("{id}")]
    public ActionResult PartialCommandUpdatead(int id, JsonPatchDocument<CommandUpdateDto> pathcDoc)
    {
        Command entity = _commandAPIRepo.GetCommandById(id);
        if (entity == null)
        {
            return NotFound();
        }
        CommandUpdateDto commandUpdateDto = _mapper.Map<CommandUpdateDto>(entity);
        pathcDoc.ApplyTo(commandUpdateDto, ModelState);
        if (!TryValidateModel(commandUpdateDto))
        {
            return ValidationProblem(ModelState);
        }
        _mapper.Map(commandUpdateDto, entity);
        _commandAPIRepo.UpdateCommand(entity);
        _commandAPIRepo.SaveChange();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteCommand(int id)
    {
        //Some Change
        Command entity = _commandAPIRepo.GetCommandById(id);
        if (entity == null)
        {
            return NotFound();
        }
        _commandAPIRepo.DeleteCommand(entity);
        _commandAPIRepo.SaveChange();
        return NoContent();
    }
}