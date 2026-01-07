using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NiotechoneCQRS.Application.ApiRoutes;
using NiotechoneCQRS.Application.DTOs.RequestDTOs;
using NiotechoneCQRS.Application.Settings.Commands.SaveConfiguration;
using NiotechoneCQRS.Application.Settings.Commands.Update;
using NiotechoneCQRS.Application.Settings.Queries.GetConfigMasterByKey;
using NiotechoneCQRS.Application.Settings.Queries.GetConfigurationListByCompanyId;

namespace NiotechoneCQRS.Web.Areas.Api.Controllers;

[Area("Api")]
[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class SettingController : ControllerBase
{
    private readonly ISender _sender;
    public SettingController(ISender sender)
    {
        _sender = sender;
    }

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

    [HttpPut(ApiRoutes.UpdateConfigurationList)]
    public async Task<IActionResult> UpdateConfigurationList([FromBody] UpdateConfigurationCommand command)
    {
        var response = await _sender.Send(command);

        if (!response.IsSuccess)
            return NotFound(response);

        return Ok(response);
    }

    [HttpPost(ApiRoutes.SaveSystemConfigurationValue)]
    public async Task<IActionResult> SaveSystemConfigurationValue(
        [FromBody] List<ConfigManagerValueRequestDTO> model)
    {
        var result = await _sender.Send(
            new SaveSystemConfigurationCommand(model)
        );

        return Ok(new { success = result });
    }

    [HttpGet(ApiRoutes.GetConfigMasterByKey)]
    public async Task<IActionResult> GetConfigMasterByKey(string configMasterKey, long companyId)
    {
        var result = await _sender.Send(
            new GetConfigMasterByKeyQuery(configMasterKey, companyId)
        );

        return Ok(result);
    }

}
