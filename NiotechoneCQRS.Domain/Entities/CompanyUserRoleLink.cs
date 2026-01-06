namespace NiotechoneCQRS.Domain.Entities;

public class CompanyUserRoleLink
{
    public long LinkId { get; set; }
    public long UserId { get; set; }
    public long CompanyId { get; set; }
    public long UserRoleId { get; set; }
    public long CreatedBy { get; set; }
    public System.DateTime CreatedDate { get; set; }
    public Nullable<long> ModifiedBy { get; set; }
    public Nullable<System.DateTime> ModifiedDate { get; set; }

    public virtual UserRole UserRole { get; set; }
    public virtual User User { get; set; }
    public virtual Company Company { get; set; }
}
