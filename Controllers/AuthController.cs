using BC = BCrypt.Net.BCrypt;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web.Models;
using web.Services;

namespace web.Controllers;

public class AuthController : Controller
{
    private readonly ILogger<AuthController> _logger;
    private readonly UsersService _usersService;

    public AuthController(ILogger<AuthController> logger, UsersService usersService)
    {
        _logger = logger;
        _usersService = usersService;
    }

    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }

    [AllowAnonymous]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(User user)
    {
        if (ModelState.IsValid && user != null)
        {
            var success = await CookieAuthentication.Login(HttpContext, _usersService, user);

            if (success)
                return RedirectToAction("Index", "Home");

        }
        return RedirectToAction("Login");
    }

    [Authorize]
    public IActionResult Logout()
    {
        CookieAuthentication.Logout(HttpContext);
        return RedirectToAction("Login");
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register(User user)
    {
        if (ModelState.IsValid && user != null)
        {
            // register user
            await _usersService.CreateAsync(new UserDTO(user));

            // authenticate user
            var success = await CookieAuthentication.Login(HttpContext, _usersService, user);

            if (success)
                return RedirectToAction("Index", "Home");

        }
        return RedirectToAction("Register");
    }
}
