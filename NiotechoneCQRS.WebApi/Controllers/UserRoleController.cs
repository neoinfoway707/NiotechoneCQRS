using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NiotechoneCQRS.Application.ApiRoutes;
using NiotechoneCQRS.Application.UserRole.Queries.GetAllRoles;

namespace NiotechoneCQRS.WebApi.Controllers;

//[Route("api/[controller]")]
[ApiController]
public class UserRoleController : ControllerBase
{
    private readonly ISender _sender;
    public UserRoleController(ISender sender)
    {
        _sender = sender;
    }

    [Authorize]
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
