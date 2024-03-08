using firstproject.Models.DTOs;
using firstproject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using firstproject.Helpers;
using System.Security.Claims;
using firstproject.Models.Domain;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ToDoController : ControllerBase {
    
    private readonly ToDoService _toDoService;
    public ToDoController(ToDoService toDoService){
        _toDoService = toDoService;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var toDos = await _toDoService.GetAllTasks();
		return Ok(toDos);
    }

    [HttpPost]
    public async Task<IActionResult> CreateToDo(CreateToDoDto createToDoDto)
    {
        long userId = TokenHelper.GetUserId(User.FindFirst(ClaimTypes.NameIdentifier));
        ToDo newTodo = await _toDoService.Create(createToDoDto, userId);
        return CreatedAtAction(null,null, newTodo);
    }

    [HttpGet]
    public async Task<IActionResult> GetFromUser()
    {
        long userId = TokenHelper.GetUserId(User.FindFirst(ClaimTypes.NameIdentifier));
        return Ok(await _toDoService.GetAllFromUser(userId));
    }

    [HttpPatch]
    public async Task<IActionResult> Toggle(ToggleToDoDto data)
    {
        long userId = TokenHelper.GetUserId(User.FindFirst(ClaimTypes.NameIdentifier));

        var toDo = await _toDoService.Toggle(data, userId);
        return Ok(toDo);
    }
}