using MediatR;
using Microsoft.AspNetCore.Mvc;
using NiotechoneCQRS.Application.ApiRoutes;
using NiotechoneCQRS.Application.Company.Queries.GetAllCompanies;
using NiotechoneCQRS.Application.Parameter.Queries.GetAllParameterValues;

namespace NiotechoneCQRS.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
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
