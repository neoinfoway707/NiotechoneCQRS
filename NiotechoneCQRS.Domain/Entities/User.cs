using System.ComponentModel.DataAnnotations.Schema;

namespace NiotechoneCQRS.Domain.Entities;

public class User
{
    public long UserId { get; set; }
    public long CompanyId { get; set; }
    public string FullName { get; set; }
    public Nullable<long> UserRoleId { get; set; }
    public string Address { get; set; }
    public Nullable<int> CountryId { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public int StatusId { get; set; }
    public Nullable<System.DateTime> CreatedDate { get; set; }
    public Nullable<long> CreatedBy { get; set; }
    public Nullable<System.DateTime> ModifiedDate { get; set; }
    public Nullable<long> ModifyBy { get; set; }
    public string PasswordDecrypt { get; set; }
    public string token { get; set; }
    public Nullable<int> UserTypeId { get; set; }
    public Nullable<byte> UserLoginType { get; set; }
    public string LoginGUID { get; set; }

    public virtual UserRole UserRole { get; set; }
    public virtual Company Company { get; set; }
    public virtual ICollection<CompanyUserRoleLink> CompanyUserRoleLinks { get; set; }

}
