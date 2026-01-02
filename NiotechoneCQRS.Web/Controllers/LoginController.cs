using Microsoft.AspNetCore.Mvc;
using NiotechoneCQRS.Application.DTOs.RequestDTOs;
using NiotechoneCQRS.Application.DTOs.ResponseDTOs;
using NiotechoneCQRS.Application.Enum;
using NiotechoneCQRS.Domain.Entities;

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
        HttpContext.Session.SetString("UserName", result!.User!.UserName);
        HttpContext.Session.SetString("Company", result!.User!.CompanyName);
        HttpContext.Session.SetInt32("CompanyId", (int)result!.User!.CompanyId);
        HttpContext.Session.SetString("JwtToken", result!.Token!);
        HttpContext.Session.SetInt32(
            "UserRoleId",
            (int)(result.User.UserRoleId));

        if (result.User.UserRoleId == (long?)Enums.UserType.SGEAdmin)
        {
            return RedirectToAction("Index", "Admin");
        }

        return RedirectToAction("Index", "Dashboard");
    }

    private async Task LoadCompanies()
    {
        var response =
            await _apiClient.GetAsync<ResponseDTO<List<Company>>>(
                "/api/Company/get-all-companies");

        ViewBag.Companies = response?.Data ?? new List<Company>();
    }
}
