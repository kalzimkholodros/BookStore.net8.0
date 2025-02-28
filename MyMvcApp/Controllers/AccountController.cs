using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using MyMvcApp.Services;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MyMvcApp.Controllers;

public class AccountController : Controller
{
    private readonly MongoDbService _mongoDbService;

    public AccountController(MongoDbService mongoDbService)
    {
        _mongoDbService = mongoDbService;
    }

    public IActionResult Login()
    {
        if (User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        var user = await _mongoDbService.GetUserByEmailAsync(email);
        if (user == null)
        {
            ModelState.AddModelError("", "Invalid email or password");
            return View();
        }

        var hashedPassword = HashPassword(password);
        if (user.Password != hashedPassword)
        {
            ModelState.AddModelError("", "Invalid email or password");
            return View();
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        return RedirectToAction("Index", "Home");
    }

    public IActionResult Register()
    {
        if (User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(User user)
    {
        if (!ModelState.IsValid)
        {
            return View(user);
        }

        // Check if email already exists
        var existingUser = await _mongoDbService.GetUserByEmailAsync(user.Email);
        if (existingUser != null)
        {
            ModelState.AddModelError("Email", "Email already exists");
            return View(user);
        }

        // Check if username already exists
        existingUser = await _mongoDbService.GetUserByUsernameAsync(user.Username);
        if (existingUser != null)
        {
            ModelState.AddModelError("Username", "Username already exists");
            return View(user);
        }

        // Hash password
        user.Password = HashPassword(user.Password);
        await _mongoDbService.CreateUserAsync(user);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Profile()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login");
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _mongoDbService.GetUserByIdAsync(userId);
        return View(user);
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }
} 