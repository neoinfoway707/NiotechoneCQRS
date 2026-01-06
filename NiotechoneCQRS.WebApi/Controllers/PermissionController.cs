using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NiotechoneCQRS.Application.ApiRoutes;
using NiotechoneCQRS.Application.Permission.Commands.SaveKpiList;
using NiotechoneCQRS.Application.Permission.Commands.SavePermissionList;
using NiotechoneCQRS.Application.Permission.Queries.GetKPIList;
using NiotechoneCQRS.Application.Permission.Queries.GetPermissionsByRole;
using NiotechoneCQRS.Domain.Entities;

namespace NiotechoneCQRS.WebApi.Controllers;

//[Route("api/[controller]")]
[ApiController]
public class PermissionController : ControllerBase
{
    private readonly ISender _sender;
    public PermissionController(ISender sender)
    {
        _sender = sender;
    }

    [Authorize]
    [HttpGet(ApiRoutes.GetPermissionsByRole)]
    public async Task<IActionResult> GetPermissionList(int roleId)
    {
        var permissions = await _sender.Send(new GetPermissionsByRoleQuery { RoleId = roleId });

        if (permissions.Data == null)
        {
            return NotFound(permissions);
        }
        return Ok(permissions);
    }

    [Authorize]
    [HttpPost(ApiRoutes.SavePermissionList)]
    public async Task<IActionResult> SavePermissionLis(SaveUserRolePermissionList permissionList)
    {
        var result = await _sender.Send(new SavePermissionListCommand(permissionList));

        if (result.Data == null)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [Authorize]
    [HttpGet(ApiRoutes.GetKpiList)]
    public async Task<IActionResult> GetKpiList(long userRoleId, long companyId)
    {
        var kpiList = await _sender.Send(new GetKpiListQuery { UserRoleId = userRoleId, CompanyId = companyId });

        if (kpiList.Data == null)
        {
            return NotFound(kpiList);
        }
        return Ok(kpiList);
    }

    [Authorize]
    [HttpPost(ApiRoutes.SaveKpiList)]
    public async Task<IActionResult> SaveKpiList(List<KPIList> kPILists)
    {
        var result = await _sender.Send(new SaveKpiListCommand(kPILists));

        if (result == null || !result.Data)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
}
