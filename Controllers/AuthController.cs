using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web.Models;
using web.Services;

namespace web.Controllers;

public class AuthController : Controller
{
    private readonly ILogger<AuthController> _logger;

    public AuthController(ILogger<AuthController> logger)
    {
        _logger = logger;
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
            var success = await CookieAuthentication.Login(HttpContext, user);

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
}
