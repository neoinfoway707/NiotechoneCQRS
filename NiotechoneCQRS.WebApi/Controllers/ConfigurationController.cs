using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NiotechoneCQRS.Application.ApiRoutes;
using NiotechoneCQRS.Application.ConfigurationManager.Queries.GetConfigMasterByKey;

namespace NiotechoneCQRS.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ConfigurationController : ControllerBase
{
    private readonly ISender _sender;
    public ConfigurationController(ISender sender)
    {
        _sender = sender;
    }

    [Authorize]
    [HttpGet(ApiRoutes.GetConfigByKey)]
    public async Task<IActionResult> GetConfigByKey(string configKey, long companyId)
    {
        var config = await _sender.Send(new GetConfigMasterByKeyQuery
        {
            ConfigMasterKey = configKey,
            CompanyId = companyId
        });

        if (config.Data == null)
        {
            return NotFound(config);
        }
        return Ok(config);
    }
}
