using System.ComponentModel.DataAnnotations;

namespace TodoAPI.DTOs;

public class UserDto
{
    [Required, MaxLength(15)]
    public required string Username { get; set; }
    [EmailAddress, MaxLength(40), Required] 
    public required string Email { get; set; }
    [DataType(DataType.Password), MinLength(8), MaxLength(25), Required]
    public required string Password { get; set; }
}