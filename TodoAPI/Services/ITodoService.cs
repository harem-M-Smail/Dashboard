using TodoAPI.DTOs;
using TodoAPI.Models;

namespace TodoAPI.Services;

public interface ITodoService
{
    Task CreateTodo(int userId, TodoDto newTodo);
    Task<List<Todo>> GetAllTodo(int userId);
    TodoPage GetTodoByPage(int userId, int page, int pageSize, string? status, string? title);
    Task<List<Todo>> SearchTodo(int userId, TodoSearch todoSearch);
    Task<Todo?> GetTodo(int userId, int todoId);
    Task<Todo?> UpdateTodo(int userId, int todoId, TodoDto todoDto);
    Task DeleteTodo(int userId, int todoId);
}