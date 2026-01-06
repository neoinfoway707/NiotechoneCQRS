using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NiotechoneCQRS.Application.ApiRoutes;
using NiotechoneCQRS.Application.DTOs.RequestDTOs;
using NiotechoneCQRS.Application.Settings.Commands.SaveConfiguration;
using NiotechoneCQRS.Application.Settings.Commands.Update;
using NiotechoneCQRS.Application.Settings.Queries;
using NiotechoneCQRS.Application.Settings.Queries.GetConfigMasterByKey;
using NiotechoneCQRS.Application.Settings.Queries.GetConfigurationListByCompanyId;
using NiotechoneCQRS.Application.User.Commands.Update;
using NiotechoneCQRS.Application.User.Queries.GetAllUsers;
using NiotechoneCQRS.Domain.Entities;
using System.Collections.Generic;

namespace NiotechoneCQRS.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SettingController : ControllerBase
{
    private readonly ISender _sender;
    public SettingController(ISender sender)
    {
        _sender = sender;
    }

    [Authorize]
    [HttpGet(ApiRoutes.ConfigurationList)]
    public async Task<IActionResult> GetConfigurationList(long companyId)
    {
        var response = await _sender.Send(new GetConfigurationListQuery { CompanyId = companyId });

        if (!response.IsSuccess || response.Data == null)
        {
            return NotFound(response);
        }
        return Ok(response);
    }

    [Authorize]
    [HttpPut(ApiRoutes.UpdateConfigurationList)]
    public async Task<IActionResult> UpdateConfigurationList([FromBody] UpdateConfigurationCommand command)
    {
        var response = await _sender.Send(command);

        if (!response.IsSuccess)
            return NotFound(response);

        return Ok(response);
    }

    [Authorize]
    [HttpPost(ApiRoutes.SaveSystemConfigurationValue)]
    public async Task<IActionResult> SaveSystemConfigurationValue(
        [FromBody] List<ConfigManagerValueRequestDTO> model)
    {
        var result = await _sender.Send(
            new SaveSystemConfigurationCommand(model)
        );

        return Ok(new { success = result });
    }

    [Authorize]
    [HttpGet(ApiRoutes.GetConfigMasterByKey)]
    public async Task<IActionResult> GetConfigMasterByKey(string configMasterKey, long companyId)
    {
        var result = await _sender.Send(
            new GetConfigMasterByKeyQuery(configMasterKey, companyId)
        );

        return Ok(result);
    }

}
