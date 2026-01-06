namespace NiotechoneCQRS.Domain.Entities;

public class Module
{
    public int ModuleId { get; set; }
    public string ModuleName { get; set; }
    public string URL { get; set; }
    public Nullable<int> Status { get; set; }
    public Nullable<int> Seq { get; set; }
    public Nullable<int> ParentModule { get; set; }
    public Nullable<int> ModuleLevel { get; set; }
    public Nullable<int> SubLevelOrder { get; set; }
    public string ModuleCode { get; set; }
    public string SubscriptionModels { get; set; }

    public virtual ICollection<UserRoleModuleOperation> UserRoleModuleOperations { get; set; }
}
