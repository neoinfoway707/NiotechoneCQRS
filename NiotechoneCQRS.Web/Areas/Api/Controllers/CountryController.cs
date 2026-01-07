using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NiotechoneCQRS.Application.ApiRoutes;
using NiotechoneCQRS.Application.Country.Queries.GetAllCountries;
using NiotechoneCQRS.Application.Country.Queries.GetAllCurrency;
using NiotechoneCQRS.Application.Country.Queries.GetLanguageList;

namespace NiotechoneCQRS.Web.Areas.Api.Controllers;

[Area("Api")]
[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class CountryController : ControllerBase
{
    private readonly ISender _sender;
    public CountryController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet(ApiRoutes.GetAllCountries)]
    public async Task<IActionResult> GetAllCountries()
    {
        var countries = await _sender.Send(new GetAllCountriesQuery());

        if (countries.Data == null)
        {
            return NotFound(countries);
        }
        return Ok(countries);
    }

    [HttpGet(ApiRoutes.GetAllCurrency)]
    public async Task<IActionResult> GetAllCurrency()
    {
        var currency = await _sender.Send(new GetAllCurrencyQuery());

        if (currency.Data == null)
        {
            return NotFound(currency);
        }
        return Ok(currency);
    }

    [HttpGet(ApiRoutes.GetLanguageList)]
    public async Task<IActionResult> GetLanguageList()
    {
        var languageList = await _sender.Send(new GetLanguageListQuery());

        if (languageList.Data == null)
        {
            return NotFound(languageList);
        }
        return Ok(languageList);
    }
}
