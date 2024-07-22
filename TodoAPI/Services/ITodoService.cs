using TodoAPI.DTOs;
using TodoAPI.Models;

namespace TodoAPI.Services;

public interface ITodoService
{
    Task CreateTodo(int userId, TodoCreationDto newTodo);
    Task<List<Todo>> GetAllTodo(int userId);
    Page GetTodoByPage(int userId, int page, int pageSize);
    Task<Todo?> GetTodo(int userId, int todoId);
    Task<Todo?> UpdateTodo(int userId, int todoId, TodoUpdateDto todoUpdateDto);
    Task DeleteTodo(int userId, int todoId);
}