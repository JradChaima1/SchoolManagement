namespace School.Services.Services;

using System.Security.Cryptography;
using System.Text;
using School.Core.Interfaces;
using School.Core.Models;

public class AuthService : IAuthService
{
    private readonly IRepository<User> _userRepository;

    public AuthService(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> LoginAsync(string username, string password)
    {
        var users = await _userRepository.GetAllAsync();
        var user = users.FirstOrDefault(u => u.Username == username && u.IsActive);

        if (user == null)
            return null;

        if (!VerifyPassword(password, user.PasswordHash))
            return null;

        return user;
    }

    public async Task<User> RegisterAsync(string username, string email, string password, int roleId)
    {
        var user = new User
        {
            Username = username,
            Email = email,
            PasswordHash = HashPassword(password),
            RoleId = roleId,
            IsActive = true,
            CreatedDate = DateTime.Now
        };

        return await _userRepository.AddAsync(user);
    }

    public async Task<bool> UserExistsAsync(string username)
    {
        var users = await _userRepository.GetAllAsync();
        return users.Any(u => u.Username == username);
    }

    public string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        var hashOfInput = HashPassword(password);
        return hashOfInput == hashedPassword;
    }
}
