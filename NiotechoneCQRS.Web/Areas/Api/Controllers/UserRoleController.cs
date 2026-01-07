using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NiotechoneCQRS.Application.ApiRoutes;
using NiotechoneCQRS.Application.UserRole.Queries.GetAllRoles;

namespace NiotechoneCQRS.Web.Areas.Api.Controllers;

[Area("Api")]
//[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UserRoleController : ControllerBase
{
    private readonly ISender _sender;
    public UserRoleController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet(ApiRoutes.GetAllRoles)]
    public async Task<IActionResult> GetAllUsers()
    {
        var roles = await _sender.Send(new GetAllRolesQuery());

        if (roles.Data == null)
        {
            return NotFound(roles);
        }
        return Ok(roles);
    }
}
