using TodoAPI.Models;

namespace TodoAPI.DTOs;

public class Page
{
    public int TotalPages { get; init; }
    public int CurrentPage { get; init; }
    
    public required List<Todo> Todos { get; init; }
}