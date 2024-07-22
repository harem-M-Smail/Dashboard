using System.Security.Claims;

namespace TodoAPI.User_Claims;

public class UserClaims
{
    public int UserId { get; init; }
    public string Username { get; init; }
    public string Email { get; init; }

    public UserClaims(ClaimsPrincipal userClaims)
    {
        UserId = Convert.ToInt32(userClaims.FindFirstValue(ClaimTypes.NameIdentifier));
        Username = userClaims.FindFirstValue(ClaimTypes.Name)!;
        Email = userClaims.FindFirstValue(ClaimTypes.Email)!;
    }
    
}