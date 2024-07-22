using Microsoft.EntityFrameworkCore;
using TodoAPI.Data_Context;
using TodoAPI.DTOs;
using TodoAPI.Models;

namespace TodoAPI.Services;

public class UserService : IUserService
{
    private readonly TodoDbContext _database;

    public UserService(TodoDbContext database)
    {
        _database = database;
    }
    
    public async Task<bool> RegisterUser(UserDto userDto)
    {
        var userExists = await _database.Users
            .Where(u => u.Username == userDto.Username || u.Email == userDto.Email)
            .ToListAsync();

        if (userExists.Count != 0)
            return false;

        var user = new User()
        {
            Username = userDto.Username,
            Email = userDto.Email,
            Password = userDto.Password
        };

        await _database.Users.AddAsync(user);
        await _database.SaveChangesAsync();

        return true;
    }

    public async Task<User?> AuthenticateUser(UserDto userDto)
    {
        var user = await _database.Users.FirstOrDefaultAsync(u =>
                u.Username == userDto.Username && u.Email == userDto.Email && u.Password == userDto.Password);

        return user;
    }

    public async Task<User> GetUserInfo(int userId)
    {
        var user = await _database.Users.FindAsync(userId);

        return user!;
    }

    public async Task<bool> DeleteUser(int userId, string confirmPassword)
    {
        var user = GetUserInfo(userId).Result;

        if (user is null) // just in-case
            return true;
        if (user.Password != confirmPassword)
            return false;

        _database.Users.Remove(user);
        await _database.SaveChangesAsync();

        return true;
    }
}