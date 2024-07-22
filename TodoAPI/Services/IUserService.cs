using TodoAPI.DTOs;
using TodoAPI.Models;

namespace TodoAPI.Services;

public interface IUserService
{
    Task<bool> RegisterUser(UserDto userDto);
    Task<User?> AuthenticateUser(UserDto userDto);
    Task<User> GetUserInfo(int userId);
    Task<bool> DeleteUser(int userId, string confirmPassword);
}