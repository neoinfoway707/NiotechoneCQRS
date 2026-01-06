namespace NiotechoneCQRS.Domain.Entities;

public class UserRoleModuleOperation
{
    public long UserRoleModuleOperationId { get; set; }
    public long UserRoleId { get; set; }
    public int ModuleId { get; set; }
    public int OperationId { get; set; }
    public int StatusId { get; set; }

    public virtual UserRole UserRole { get; set; }
    public virtual Module Module { get; set; }
}
