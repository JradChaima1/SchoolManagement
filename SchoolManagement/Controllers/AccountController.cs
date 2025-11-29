using Microsoft.AspNetCore.Mvc;
using School.Core.Interfaces;

namespace SchoolManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IRepository<School.Core.Models.Role> _roleRepository;

        public AccountController(IAuthService authService, IRepository<School.Core.Models.Role> roleRepository)
        {
            _authService = authService;
            _roleRepository = roleRepository;
        }

        public IActionResult Login()
        {
            return View();
        }

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Login(string username, string password)
{
    var user = await _authService.LoginAsync(username, password);

    if (user == null)
    {
        ViewBag.Error = "Invalid username or password";
        return View();
    }

    var roles = await _roleRepository.GetAllAsync();
    var userRole = roles.FirstOrDefault(r => r.Id == user.RoleId);

    HttpContext.Session.SetInt32("UserId", user.Id);
    HttpContext.Session.SetString("Username", user.Username);
    HttpContext.Session.SetInt32("RoleId", user.RoleId);
    HttpContext.Session.SetString("RoleName", userRole?.Name ?? "User");

    return RedirectToAction("Index", "Dashboard");
}

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string username, string email, string password, string confirmPassword)
{
    if (password != confirmPassword)
    {
        ViewBag.Error = "Passwords do not match";
        return View();
    }

    if (await _authService.UserExistsAsync(username))
    {
        ViewBag.Error = "Username already exists";
        return View();
    }

    var roles = await _roleRepository.GetAllAsync();
    var adminRole = roles.FirstOrDefault(r => r.Name == "Admin");
    
    if (adminRole == null)
    {
        adminRole = await _roleRepository.AddAsync(new School.Core.Models.Role 
        { 
            Name = "Admin", 
            Description = "School Administrator" 
        });
    }

    await _authService.RegisterAsync(username, email, password, adminRole.Id);

    return RedirectToAction("Login");
}


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
