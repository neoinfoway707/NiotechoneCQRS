using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NiotechoneCQRS.Application.ApiRoutes;
using NiotechoneCQRS.Application.DTOs.ResponseDTOs;
using NiotechoneCQRS.Web.Helper;

namespace NiotechoneCQRS.Web.Controllers;

[Authorize]
public class UserRoleController : Controller
{
    private readonly ApiClient _apiClient;
    private readonly AppSettings _setting;
    public UserRoleController(ApiClient apiClient, AppSettings settings)
    {
        _apiClient = apiClient;
        _setting = settings;
    }

    public async Task<IActionResult> Index()
    {
        string url = _setting.BaseUrl + ApiRoutes.GetAllRoles;
        var userList = await _apiClient.GetAsync<UserRoleListModel>(url);
        return View(userList);
    }
}
