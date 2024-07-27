using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TodoAPI.DTOs;
using TodoAPI.Services;
using TodoAPI.User_Claims;

namespace TodoAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TodoController : ControllerBase
{
    private readonly ITodoService _todoService;
    private readonly UserClaims _userClaims;    

    public TodoController(ITodoService todoService, IHttpContextAccessor contextAccessor)
    {
        _todoService = todoService;
        _userClaims = new UserClaims(contextAccessor.HttpContext!.User);
    }

    [HttpGet("todos")]
    public async Task<IActionResult> GetAllTodos()
    {
        var todos = await _todoService.GetAllTodo(_userClaims.UserId);

        return Ok(todos);
    }

    [HttpGet("todos-page/{page:int}/{pageSize:int}/{status}/{title?}")]
    public IActionResult GetTodosByPage(int page, int pageSize, string status, string? title=null)
    {
        var todos = _todoService.GetTodoByPage(_userClaims.UserId, page, pageSize, status, title);
        return Ok(todos);
    }   

    [HttpGet("search")]
    public async Task<IActionResult> SearchTodos(TodoSearch todoSearch)
    {
        return Ok(await _todoService.SearchTodo(_userClaims.UserId, todoSearch));
    }

    [HttpGet("todo/{todoId:int}")]
    public async Task<IActionResult> GetTodo(int todoId)
    {
        var todo = await _todoService.GetTodo(_userClaims.UserId, todoId);

        if (todo is null)
            return NotFound();
        return Ok(todo);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateTodo(TodoDto newTodo)
    {
        await _todoService.CreateTodo(_userClaims.UserId, newTodo);

        return Created();
    }

    [HttpPut("update/{todoId:int}")]
    public async Task<IActionResult> UpdateTodo(int todoId, TodoDto todo)
    {
        var updatedTodo = await _todoService.UpdateTodo(_userClaims.UserId, todoId, todo);

        if (todo is null)
            return BadRequest("Invalid Todo Id!");
        
        return Ok(updatedTodo);
    }

    [HttpDelete("todo/{todoId:int}")]
    public async Task<IActionResult> DeleteTodo(int todoId)
    {
        await _todoService.DeleteTodo(_userClaims.UserId, todoId);

        return NoContent();
    }
    
}