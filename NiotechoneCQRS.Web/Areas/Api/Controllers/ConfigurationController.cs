using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NiotechoneCQRS.Application.ApiRoutes;
using NiotechoneCQRS.Application.ConfigurationManager.Queries.GetConfigMasterByKey;

namespace NiotechoneCQRS.Web.Areas.Api.Controllers;

[Area("Api")]
[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ConfigurationController : ControllerBase
{
    private readonly ISender _sender;
    public ConfigurationController(ISender sender)
    {
        _sender = sender;
    }

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
