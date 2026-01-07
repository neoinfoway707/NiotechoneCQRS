using System.ComponentModel.DataAnnotations;
using CompanyRes = NiotechoneCQRS.Utility.Company.Company;

namespace NiotechoneCQRS.Web.Models;

public class CompanyViewModel
{
    public long? CompanyId { get; set; }

    [Required(ErrorMessageResourceType = typeof(CompanyRes), ErrorMessageResourceName = nameof(CompanyRes.CompanyNameRequired))]
    public string? CompanyName { get; set; }

    [Required(ErrorMessageResourceType = typeof(CompanyRes), ErrorMessageResourceName = nameof(CompanyRes.AddressRequired))]
    public string? Address { get; set; }

    [Required(ErrorMessageResourceType = typeof(CompanyRes), ErrorMessageResourceName = nameof(CompanyRes.CountryRequired))]
    public int? Country { get; set; }

    [Required(ErrorMessageResourceType = typeof(CompanyRes), ErrorMessageResourceName = nameof(CompanyRes.TimezoneRequired))]
    public string? TimeZone { get; set; }

    [Required(ErrorMessageResourceType = typeof(CompanyRes), ErrorMessageResourceName = nameof(CompanyRes.CityRequired))]
    public string? City { get; set; }

    [Required(ErrorMessageResourceType = typeof(CompanyRes), ErrorMessageResourceName = nameof(CompanyRes.POBoxRequired))]
    public string? POBox { get; set; }

    [Required(ErrorMessageResourceType = typeof(CompanyRes), ErrorMessageResourceName = nameof(CompanyRes.PhoneRequired))]
    public string? Phone { get; set; }
    public bool IsActive { get; set; }
    public bool Billable { get; set; }
    public bool WorkRequest { get; set; }

    public string? WorkRequestUrl { get; set; }

    [Required(ErrorMessageResourceType = typeof(CompanyRes), ErrorMessageResourceName = nameof(CompanyRes.ThresholdRequired))]
    public string? ThresholdValue { get; set; }
    public string? VAT {  get; set; }
    public string? PurchaseReqEmails { get; set; }

    [Required(ErrorMessageResourceType = typeof(CompanyRes), ErrorMessageResourceName = nameof(CompanyRes.CurrencyRequired))]
    public int? Currency { get; set; }

    public string? TaxRegistrationNo { get; set; }

    public IFormFile? CompanyLogo { get; set; }
    public int? Language { get; set; }

    public IFormFile? Header { get; set; }
    public IFormFile? Footer { get; set; }
}
