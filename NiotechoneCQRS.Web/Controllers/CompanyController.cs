using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NiotechoneCQRS.Application.ApiRoutes;
using NiotechoneCQRS.Application.DTOs.ResponseDTOs;
using NiotechoneCQRS.Domain.Entities;
using NiotechoneCQRS.Domain.Enum;
using NiotechoneCQRS.Utility.Company;
using NiotechoneCQRS.Web.Helper;
using NiotechoneCQRS.Web.Models;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using static NiotechoneCQRS.Domain.Enum.Enums;

namespace NiotechoneCQRS.Web.Controllers;

public class CompanyController : Controller
{
    private readonly ApiClient _apiClient;
    private readonly AppSettings _setting;

    public CompanyController(ApiClient apiClient, AppSettings settings)
    {
        _apiClient = apiClient;
        _setting = settings;
    }

    public async Task<IActionResult> Index()
    {
        string url = _setting.BaseUrl + "api/Company/" + ApiRoutes.GetAllCompanies;
        var companyList = await _apiClient.GetAsync<CompanyListModelDTO>(url);
        return View(companyList);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await PopulateDropdowns();

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CompanyViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await PopulateDropdowns();
            return View(model);
        }

        var company = new
        {
            CompanyName = model.CompanyName!,
            Address = model.Address,
            City = model.City,
            CountryId = model.Country ?? 0,
            TimeZone = model.TimeZone!,
            POBox = model.POBox,
            Phone = model.Phone,
            StatusId = model.IsActive ? 1 : 0,
            Billable = model.Billable,
            WorkRequest = model.WorkRequest,
            WorkRequestURL = model.WorkRequestUrl,
            CurrencyId = model.Currency ?? 0,
            ThresholdValue = int.TryParse(model.ThresholdValue, out int threshVal) ? threshVal : (int?)null,
            VAT = decimal.TryParse(model.VAT, out decimal vatVal) ? vatVal : 0,
            TaxRegistrationNo = model.TaxRegistrationNo,
            LanguageId = model.Language,
            PurchaseReqEmails = model.PurchaseReqEmails,
            LogoFile = model.CompanyLogo != null ? new
            {
                Content = ConvertFileToBytes(model.CompanyLogo),
                FileName = model.CompanyLogo.FileName,
                ContentType = model.CompanyLogo.ContentType
            } : null
        };

        string url = _setting.BaseUrl + "api/Company/" + ApiRoutes.CreateCompany;

        var result = await _apiClient.PostAsync<object, ResponseDTO<bool>>(url, company);

        if (result != null && result.Data)
        {
            return RedirectToAction("Index", "Company");
        }

        await PopulateDropdowns();
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        await PopulateDropdowns();

        string configurl =
            $"{_setting.BaseUrl}api/Configuration/{ApiRoutes.GetConfigByKey}" +
            $"?configKey={Uri.EscapeDataString(ConfigMasterKey.Enable_Company_Header_And_Footer.ToString())}" +
            $"&companyId={id}";
        var config = await _apiClient.GetAsync<ResponseDTO<ConfigManagerValue>>(configurl);

        ViewBag.ConfigForHeaderFooter = config.Data.Value;

        string url = _setting.BaseUrl + "api/Company/" +  ApiRoutes.GetCompanyById + $"?id={id}";
        var response = await _apiClient.GetAsync<ResponseDTO<Domain.Entities.Company>>(url);

        HttpContext.Session.SetInt32("CompanyId", (int)response.Data.CompanyId);
        var model = new CompanyViewModel
        {
            CompanyId = response.Data.CompanyId,
            CompanyName = response.Data.CompanyName,
            Address = response.Data.Address,
            City = response.Data.City,
            Country = response.Data.CountryId,
            TimeZone = response.Data.Timezone,
            POBox = response.Data.POBox,
            Phone = response.Data.Phone,
            IsActive = response.Data.StatusId == 1,
            Billable = response.Data.Billable ?? false,
            WorkRequest = response.Data.WorkRequest ?? false,
            WorkRequestUrl = response.Data.WorkRequestURL,
            Currency = response.Data.CurrencyId,
            ThresholdValue = response.Data.ThresholdValue?.ToString(),
            VAT = response.Data.VAT?.ToString(),
            TaxRegistrationNo = response.Data.TaxRegistrationNo,
            PurchaseReqEmails = response.Data.PurchaseReqEmails,
            Language = response.Data.LanguageId ?? 0,
        };

        if (response.Data.CompanyLogos?.Any() == true)
        {
            var logo = response.Data.CompanyLogos.First();
            ViewBag.ExistingLogo = $"data:{logo.ContentType};base64,{Convert.ToBase64String(logo.CompanyImage)}";
        }

        if (response.Data.CompanyArtifacts?.Any() == true)
        {
            var headerArtifact = response.Data.CompanyArtifacts
                    .FirstOrDefault(a => a.ArtifactType == (int)ArtifactType.CompanyHeader);

            var footerArtifact = response.Data.CompanyArtifacts
                .FirstOrDefault(a => a.ArtifactType == (int)ArtifactType.CompanyFooter);

            // You can pass these to ViewBag or map into your ViewModel
            if (headerArtifact != null)
                ViewBag.HeaderArtifact = $"data:{headerArtifact.ContentType};base64,{Convert.ToBase64String(headerArtifact.CompanyImage)}";

            if (footerArtifact != null)
                ViewBag.FooterArtifact = $"data:{footerArtifact.ContentType};base64,{Convert.ToBase64String(footerArtifact.CompanyImage)}";
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CompanyViewModel model)
    {
        string configurl =
            $"{_setting.BaseUrl}api/Configuration/{ApiRoutes.GetConfigByKey}" +
            $"?configKey={Uri.EscapeDataString(ConfigMasterKey.Enable_Company_Header_And_Footer.ToString())}" +
            $"&companyId={model.CompanyId}";
        var config = await _apiClient.GetAsync<ResponseDTO<ConfigManagerValue>>(configurl);

        if (!ModelState.IsValid)
        {
            await PopulateDropdowns();
            ViewBag.ConfigForHeaderFooter = config.Data.Value;
            ViewBag.IsValidationError = true;
            return View(model);
        }

        var company = new
        {
            CompanyName = model.CompanyName!,
            Address = model.Address,
            City = model.City,
            CountryId = model.Country ?? 0,
            TimeZone = model.TimeZone!,
            POBox = model.POBox,
            Phone = model.Phone,
            StatusId = model.IsActive ? 1 : 0,
            Billable = model.Billable,
            WorkRequest = model.WorkRequest,
            WorkRequestURL = model.WorkRequestUrl,
            CurrencyId = model.Currency ?? 0,
            ThresholdValue = int.TryParse(model.ThresholdValue, out int threshVal) ? threshVal : (int?)null,
            VAT = decimal.TryParse(model.VAT, out decimal vatVal) ? vatVal : 0,
            TaxRegistrationNo = model.TaxRegistrationNo,
            LanguageId = model.Language,
            PurchaseReqEmails = model.PurchaseReqEmails,
            LogoFile = model.CompanyLogo != null ? new
            {
                LogoContent = ConvertFileToBytes(model.CompanyLogo),
                LogoFileName = model.CompanyLogo.FileName,
                LogoContentType = model.CompanyLogo.ContentType
            } : null,
            HeaderFile = model.Header != null ? new
            {
                HeaderContent = ConvertFileToBytes(model.Header),
                HeaderFileName = model.Header.FileName,
                HeaderContentType = model.Header.ContentType
            } : null,
            FooterFile = model.Footer !=null ? new
            {
                FooterContent = ConvertFileToBytes(model.Footer),
                FooterFileName = model.Footer.FileName,
                FooterContentType = model.Footer.ContentType
            } : null
        };

        string url = _setting.BaseUrl + "api/Company/" + ApiRoutes.UpdateCompany + $"?id={model.CompanyId}";

        var result = await _apiClient.PutAsync<object, ResponseDTO<bool>>(url, company);

        if (result != null && result.Data)
        {
            return RedirectToAction("Index", "Company");
        }

        await PopulateDropdowns();
        ViewBag.ConfigForHeaderFooter = config.Data.Value;
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        string url = _setting.BaseUrl + "api/Company/" + ApiRoutes.DeleteCompany + $"?id={id}";
        var isDeleted = await _apiClient.DeleteAsync(url);

        if (!isDeleted)
        {
            TempData["Error"] = "Cannot delete company because users exist.";
            return RedirectToAction("Index", "Company");
        }

        TempData["Success"] = "Company deleted successfully";
        return RedirectToAction("Index", "Company");
    }

    private async Task PopulateDropdowns()
    {
        string counrtyurl = _setting.BaseUrl + "api/Country/" + ApiRoutes.GetAllCountries;
        var countriesList = await _apiClient.GetAsync<ResponseDTO<IList<Country>>>(counrtyurl);
        ViewBag.Countries = countriesList?.Data ?? new List<Country>();

        string currencyurl = _setting.BaseUrl + "api/Country/" + ApiRoutes.GetAllCurrency;
        var currencyList = await _apiClient.GetAsync<ResponseDTO<IList<ParameterValue>>>(currencyurl);
        ViewBag.Currency = currencyList?.Data ?? new List<ParameterValue>();

        var timeZones = TimeZoneInfo.GetSystemTimeZones();
        var timezoneList = timeZones.Select(tz => new SelectListItem
        {
            Value = tz.Id,
            Text = tz.DisplayName
        }).ToList();
        ViewBag.TimeZones = timezoneList;

        string languageurl = _setting.BaseUrl + "api/Country/" + ApiRoutes.GetLanguageList;
        var languages = await _apiClient.GetAsync<ResponseDTO<IList<ParameterValue>>>(languageurl);
        ViewBag.Language = languages?.Data.Select(l => new SelectListItem
        {
            Value = l.ParameterValuesId.ToString(),
            Text = l.Name
        }).ToList() ?? new List<SelectListItem>();
    }

    private byte[] ConvertFileToBytes(IFormFile file)
    {
        using var ms = new MemoryStream();
        file.CopyTo(ms);
        return ms.ToArray();
    }

}
