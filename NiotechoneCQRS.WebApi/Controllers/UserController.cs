using MediatR;
using Microsoft.AspNetCore.Mvc;
using NiotechoneCQRS.Application.ApiRoutes;
using NiotechoneCQRS.Application.User.Queries.GetAllUsers;
using System.Reflection.Metadata;

namespace NiotechoneCQRS.WebApi.Controllers;

public class UserController : ControllerBase
{
    private readonly ISender _sender;
    public UserController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet(ApiRoutes.GetAllUsers)]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _sender.Send(new GetAllUsersQuery());

        if (users.Data == null)
        {
            return NotFound(users);
        }
        return Ok(users);
    }
}
