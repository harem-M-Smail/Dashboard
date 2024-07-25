using System.ComponentModel.DataAnnotations;

namespace TodoAPI.Models;

public class User
{
    [Key]
    public int Id { get; init; }
    [MaxLength(15)]
    public required string Username { get; init; }
    [EmailAddress, MaxLength(40)] 
    public required string Email { get; init; }
    [DataType(DataType.Password), MinLength(8), MaxLength(25)]
    public required string Password { get; set; }

    public int TodoCount { get; set; } = 0;
    public uint ProductCount { get; set; } = 0;
}