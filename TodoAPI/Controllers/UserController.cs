using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoAPI.DTOs;
using TodoAPI.Services;
using TodoAPI.User_Claims;

namespace TodoAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly UserClaims _userClaims;

    public UserController(IUserService userService, IHttpContextAccessor contextAccessor)
    {
        _userService = userService;
        _userClaims = new UserClaims(contextAccessor.HttpContext!.User);
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(UserDto userDto)
    {
        if(!ModelState.IsValid)
            return BadRequest("failed to create the user");
        
        if(await _userService.RegisterUser(userDto) == false)
            return BadRequest("username/email already exists!");
        
        return Created();
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser(UserDto userDto)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        var user = await _userService.AuthenticateUser(userDto);

        if (user is null)
            return Unauthorized("Incorrect username/email/password");

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.Id)),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email)
        };
        
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        
        var claimsPrincipal = new ClaimsPrincipal(identity);
        
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
            claimsPrincipal, new AuthenticationProperties() {IsPersistent = true});

        return SignIn(claimsPrincipal);
    }
    
    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> LogoutUser()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return SignOut();
    }

    [HttpGet("info")]
    // [Authorize]
    public async Task<IActionResult> GetUserInfo()
    {
        var user = await _userService.GetUserInfo(_userClaims.UserId);

        return Ok(user);
    }

    [HttpDelete("delete/{confirmPassword}")]
    [Authorize]
    public async Task<IActionResult> DeleteUser(string confirmPassword)
    {
        if (await _userService.DeleteUser(_userClaims.UserId, confirmPassword) == false)
            return StatusCode(403, new { error = "Incorrect Password" });

        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return SignOut();
    }
    
    
}