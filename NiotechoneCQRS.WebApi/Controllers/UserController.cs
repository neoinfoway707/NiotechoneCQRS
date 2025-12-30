using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NiotechoneCQRS.Application.ApiRoutes;
using NiotechoneCQRS.Application.User.Commands.Create;
using NiotechoneCQRS.Application.User.Commands.Delete;
using NiotechoneCQRS.Application.User.Commands.Update;
using NiotechoneCQRS.Application.User.Queries.GetAllUsers;
using NiotechoneCQRS.Application.User.Queries.GetUserById;
using System.Reflection.Metadata;

namespace NiotechoneCQRS.WebApi.Controllers;

public class UserController : ControllerBase
{
    private readonly ISender _sender;
    public UserController(ISender sender)
    {
        _sender = sender;
    }

    [Authorize]
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
    [Authorize]
    [HttpGet(ApiRoutes.GetUserById)]
    public async Task<IActionResult> GetUserById(string id)
    {
        var user = await _sender.Send(new GetUserByIdQuery { Id = id });

        if (user.Data == null)
        {
            return NotFound(user);
        }
        return Ok(user);
    }

    [Authorize]
    [HttpDelete(ApiRoutes.DeleteUserById)]
    public async Task<IActionResult> DeleteUserById(string id)
    {
        var user = await _sender.Send(new DeleteUserByIdCommand { Id = id });

        if (user == null || !user.Data)
        {
            return NotFound(user);
        }
        return Ok(user);
    }

    [Authorize]
    [HttpPost(ApiRoutes.Create)]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        var user = await _sender.Send(command);

        if (user == null || !user.Data)
        {
            return NotFound(user);
        }
        return Ok(user);
    }

    [Authorize]
    [HttpPut(ApiRoutes.Update)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserCommand command)
    {
        command.Id = id;
        var user = await _sender.Send(command);

        if (user == null || !user.Data)
        {
            return NotFound(user);
        }
        return Ok(user);
    }
}
