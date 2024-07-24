using System.ComponentModel.DataAnnotations;

namespace TodoAPI.DTOs;

public class TodoCreationDto
{
    [MaxLength(50)]
    public required string Title { get; set; }
    [MaxLength(150), DataType(DataType.MultilineText)]
    public required string Description { get; set; }
    public required string Status { get; set; }
}