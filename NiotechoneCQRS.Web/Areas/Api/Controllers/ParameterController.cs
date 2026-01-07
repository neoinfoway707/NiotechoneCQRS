using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NiotechoneCQRS.Application.ApiRoutes;
using NiotechoneCQRS.Application.Parameter.Queries.GetAllParameterValues;

namespace NiotechoneCQRS.Web.Areas.Api.Controllers;

[Area("Api")]
[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ParameterController : ControllerBase
{
    private readonly ISender _sender;
    public ParameterController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet(ApiRoutes.GetParameterValues)]
    public async Task<IActionResult> GetParameterValues()
    {
        var parameterName = await _sender.Send(new GetAllParameterValuesQuery());

        if (parameterName.Data == null)
        {
            return NotFound(parameterName);
        }
        return Ok(parameterName);
    }
}
