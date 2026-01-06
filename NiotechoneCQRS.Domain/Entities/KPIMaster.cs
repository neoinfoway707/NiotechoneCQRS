namespace NiotechoneCQRS.Domain.Entities;

public class KPIMaster
{
    public int KPIMasterId { get; set; }
    public string Name { get; set; }
    public string Control { get; set; }
    public string KPIDataType { get; set; }
    public string DefaultDateRange { get; set; }
    public string ChartType { get; set; }
    public Nullable<int> RedirectionToGrid { get; set; }

    public virtual ICollection<KPIAllocation> KPIAllocations { get; set; }
}
