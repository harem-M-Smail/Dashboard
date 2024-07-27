using System.ComponentModel.DataAnnotations;

namespace TodoAPI.DTOs;

public class TodoSearch
{
    [MaxLength(50)]
    public string? Title { get; set; }
    [MaxLength(15)]
    public string? Status { get; set; } 
}