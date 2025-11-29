namespace School.Core.Interfaces;

using School.Core.Models;

public interface IAuthService
{
    Task<User?> LoginAsync(string username, string password);
    Task<User> RegisterAsync(string username, string email, string password, int roleId);
    Task<bool> UserExistsAsync(string username);
    string HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword);
}
