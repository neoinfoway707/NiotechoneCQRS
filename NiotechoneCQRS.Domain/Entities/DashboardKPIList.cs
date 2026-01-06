namespace NiotechoneCQRS.Domain.Entities;

public class DashboardKPIList
{
    public Nullable<int> KPIAllocationId { get; set; }
    public Nullable<long> CompanyId { get; set; }
    public Nullable<long> UserRoleId { get; set; }
    public int KPIMasterId { get; set; }
    public string KPIDataType { get; set; }
    public string DefaultDateRange { get; set; }
    public Nullable<int> RedirectionToGrid { get; set; }
    public string Name { get; set; }
    public string Control { get; set; }
    public string ChartType { get; set; }
    public Nullable<int> OrderInDashboard { get; set; }
}
