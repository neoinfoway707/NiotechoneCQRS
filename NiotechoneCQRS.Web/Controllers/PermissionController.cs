using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NiotechoneCQRS.Application.ApiRoutes;
using NiotechoneCQRS.Application.DTOs.ResponseDTOs;
using NiotechoneCQRS.Web.Helper;

namespace NiotechoneCQRS.Web.Controllers;

[Authorize]
public class PermissionController : Controller
{
    private readonly ApiClient _apiClient;
    private readonly AppSettings _setting;

    public PermissionController(ApiClient apiClient, AppSettings setting)
    {
        _apiClient = apiClient;
        _setting = setting;
    }
    public async Task<IActionResult> Index(int roleId)
    {
        var url = $"{_setting.BaseUrl}{ApiRoutes.GetPermissionsByRole}?roleId={roleId}";
        var permissionList = await _apiClient.GetAsync<UserRoleModuleOperationModel>(url);
        return View(permissionList);
    }
}
