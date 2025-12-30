using MediatR;
using Microsoft.AspNetCore.Mvc;
using NiotechoneCQRS.Application.ApiRoutes;
using NiotechoneCQRS.Application.Company.Queries.GetAllCompanies;

namespace NiotechoneCQRS.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class CompanyController : ControllerBase
{
    private readonly ISender _sender;
    public CompanyController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet(ApiRoutes.GetAllCompanies)]
    public async Task<IActionResult> GetAllCompanies()
    {
        var companies = await _sender.Send(new GetAllCompaniesQuery());

        if (companies.Data == null)
        {
            return NotFound(companies);
        }
        return Ok(companies);
    }
}
