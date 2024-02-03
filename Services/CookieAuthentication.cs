using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using web.Models;
using web.Repositories;

namespace web.Services;

public static class CookieAuthentication
{
    public static async Task<bool> Login(HttpContext httpContext, User user)
    {
        var authenticatedUser = UserRepository.AuthenticateUser(user.Email, user.Password);

        if(authenticatedUser == null || string.IsNullOrEmpty(authenticatedUser.Email)){
            return false;
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, authenticatedUser.Email),
            new Claim(ClaimTypes.Role, authenticatedUser.Role),
            new Claim("FullName", authenticatedUser.FullName)
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await httpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            new AuthenticationProperties{}
        );
        
        return true;
    }

    public static async void Logout(HttpContext httpContext)
    {
        await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}
