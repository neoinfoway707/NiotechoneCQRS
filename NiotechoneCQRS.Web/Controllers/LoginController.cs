using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NiotechoneCQRS.Application.DTOs.RequestDTOs;
using NiotechoneCQRS.Application.DTOs.ResponseDTOs;
using NiotechoneCQRS.Domain.Entities;
using NiotechoneCQRS.Domain.Enum;
using System.Security.Claims;

namespace NiotechoneCQRS.Web.Controllers;

public class LoginController : Controller
{
    private readonly ApiClient _apiClient;
    public LoginController(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<IActionResult> Index()
    {
        await LoadCompanies();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequestDTO login)
    {
        if (!ModelState.IsValid)
        {
            await LoadCompanies();
            return View("Index", login);
        }

        var result = await _apiClient
            .PostAsync<LoginRequestDTO, LoginApiResponse>("/login", login);

        if (result == null)
        {
            ModelState.AddModelError("", "Invalid login credentials");
            await LoadCompanies();
            return View("Index", login);
        }

        HttpContext.Session.SetString("JwtToken", result.Token!);
        HttpContext.Session.SetInt32("UserRoleId", (int)result.User.UserRoleId);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, result.User.UserId.ToString()),
            new Claim(ClaimTypes.Name, result.User.UserName),
            new Claim(ClaimTypes.Role, result.User.UserRoleId.ToString())
        };

        var identity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme
        );

        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal,
            new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
            }
        );

        if ((Enums.UserRole)result.User.UserRoleId == Enums.UserRole.SuperAdmin)
        {
            return RedirectToAction("Index", "Company");
        }

        return RedirectToAction("Index", "Dashboard");
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme
        );

        HttpContext.Session.Clear();

        return RedirectToAction("Index", "Login");
    }

    private async Task LoadCompanies()
    {
        var response =
            await _apiClient.GetAsync<ResponseDTO<List<Company>>>(
                "/api/Company/get-all-companies");

        ViewBag.Companies = response?.Data ?? new List<Company>();
    }
}
