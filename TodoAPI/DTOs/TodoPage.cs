using TodoAPI.Models;

namespace TodoAPI.DTOs;

public class TodoPage
{
    public int TotalPages { get; init; }
    public int CurrentPage { get; init; }
    
    public required List<Todo> Todos { get; init; }
}