using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NiotechoneCQRS.Application.ApiRoutes;
using NiotechoneCQRS.Application.Company.Commands.Create;
using NiotechoneCQRS.Application.Company.Commands.Delete;
using NiotechoneCQRS.Application.Company.Commands.Update;
using NiotechoneCQRS.Application.Company.Queries.GetAllCompanies;
using NiotechoneCQRS.Application.Company.Queries.GetCompanyById;
using NiotechoneCQRS.Application.User.Commands.Delete;

namespace NiotechoneCQRS.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
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

    [Authorize]
    [HttpGet(ApiRoutes.GetCompanyById)]
    public async Task<IActionResult> GetCompanyById(int id)
    {
        var company = await _sender.Send(new GetCompanyByIdQuery { Id = id });

        if (company.Data == null)
        {
            return NotFound(company);
        }
        return Ok(company);
    }

    [Authorize]
    [HttpPost(ApiRoutes.CreateCompany)]
    public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyCommand command)
    {
        var company = await _sender.Send(command);

        if (company == null || !company.Data)
        {
            return NotFound(company);
        }
        return Ok(company);
    }

    [Authorize]
    [HttpPut(ApiRoutes.UpdateCompany)]
    public async Task<IActionResult> UpdateCompany(int id, [FromBody] UpdateCompanyCommand command)
    {
        command.Id = id;
        var company = await _sender.Send(command);

        if (company == null || !company.Data)
        {
            return NotFound(company);
        }
        return Ok(company);
    }

    [Authorize]
    [HttpDelete(ApiRoutes.DeleteCompany)]
    public async Task<IActionResult> DeleteCompany(int id)
    {
        var company = await _sender.Send(new DeleteCompanyCommand { Id = id });

        if (company == null || !company.Data)
        {
            return NotFound(company);
        }
        return Ok(company);
    }
}
