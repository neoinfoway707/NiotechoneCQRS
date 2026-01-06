namespace NiotechoneCQRS.Domain.Entities;

public class GetPermissionListResult
{
    public int ModuleId { get; set; }
    public string ModuleName { get; set; }
    public string ModuleCode { get; set; }
    public string Operation { get; set; }
    public string OperationId { get; set; }
    public Nullable<int> ParentModule { get; set; }
    public Nullable<int> ModuleLevel { get; set; }
    public Nullable<int> SubLevelOrder { get; set; }
    public Nullable<int> Seq { get; set; }
}
