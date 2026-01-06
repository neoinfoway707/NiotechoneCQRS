namespace NiotechoneCQRS.Domain.Entities;

public class KPIAllocation
{
    public int KPIAllocationId { get; set; }
    public long CompanyId { get; set; }
    public long UserRoleId { get; set; }
    public int KPIMasterId { get; set; }
    public Nullable<int> OrderInDashboard { get; set; }

    public virtual KPIMaster KPIMaster { get; set; }
    public virtual UserRole UserRole { get; set; }
    public virtual Company Company { get; set; }
}
