using System.ComponentModel.DataAnnotations;

namespace TodoAPI.DTOs;

public class TodoUpdateDto
{
    [MaxLength(50)]
    public required string Title { get; set; }
    [MaxLength(150), DataType(DataType.MultilineText)]
    public required string Description { get; set; }
    [MaxLength(15)]
    public required string Status { get; set; } 
}