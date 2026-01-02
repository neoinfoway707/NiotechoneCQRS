using System.ComponentModel.DataAnnotations;

namespace NiotechoneCQRS.Web.Models;

public class UserViewModel
{
    public int? UserId { get; set; }
    [Required(ErrorMessage = "CompanyId is required.")]
    public long? CompanyId { get; set; }

    [Required(ErrorMessage = "Full name is required.")]
    [StringLength(100, ErrorMessage = "Full name cannot exceed 100 characters.")]
    public string? FullName { get; set; }

    [Required(ErrorMessage = "User name is required.")]
    [StringLength(50, ErrorMessage = "User name cannot exceed 50 characters.")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
    public string? Email { get; set; }

    [Phone(ErrorMessage = "Invalid phone number.")]
    [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters.")]
    public string? Phone { get; set; }

    [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
    public string? Address { get; set; }

    [Required(ErrorMessage = "Status is required.")]
    public int? StatusId { get; set; }

    [Required(ErrorMessage = "User type is required.")]
    public int? UserTypeId { get; set; }
}
