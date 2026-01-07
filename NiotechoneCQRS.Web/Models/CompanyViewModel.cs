using System.ComponentModel.DataAnnotations;

namespace NiotechoneCQRS.Web.Models;

public class CompanyViewModel
{
    public long? CompanyId { get; set; }

    [Required(ErrorMessage = "Company Name is required.")]
    public string? CompanyName { get; set; }

    [Required(ErrorMessage = "Address is required.")]
    public string? Address { get; set; }

    [Required(ErrorMessage = "Country is required.")]
    public int? Country { get; set; }

    [Required(ErrorMessage = "Time Zone is required.")]
    public string? TimeZone { get; set; }

    [Required(ErrorMessage = "City is required.")]
    public string? City { get; set; }

    [Required(ErrorMessage = "P.O. Box is required.")]
    public string? POBox { get; set; }

    [Required(ErrorMessage = "Phone is required.")]
    public string? Phone { get; set; }
    public bool IsActive { get; set; }
    public bool Billable { get; set; }
    public bool WorkRequest { get; set; }

    public string? WorkRequestUrl { get; set; }

    [Required(ErrorMessage = "Threshold Value is required.")]
    public string? ThresholdValue { get; set; }
    public string? VAT {  get; set; }
    public string? PurchaseReqEmails { get; set; }

    [Required(ErrorMessage = "Currency is required.")]
    public int? Currency { get; set; }

    public string? TaxRegistrationNo { get; set; }

    public IFormFile? CompanyLogo { get; set; }
    public int Language { get; set; }

    public IFormFile? Header { get; set; }
    public IFormFile? Footer { get; set; }
}
