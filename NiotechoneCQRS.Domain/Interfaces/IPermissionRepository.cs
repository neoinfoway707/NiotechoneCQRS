using NiotechoneCQRS.Domain.Entities;

namespace NiotechoneCQRS.Domain.Interfaces;

public interface IPermissionRepository
{
    Task<List<UserRoleModuleOperation>> GetPermissionList(int userRoleId, CancellationToken cancellationToken = default);
    Task<string?> SavePermissionList(SaveUserRolePermissionList saveUserRolePermissionList, CancellationToken cancellationToken = default);
    Task<List<KPIList>> GetKpiList(long companyId, long userRoleId, CancellationToken cancellationToken = default);
    Task<bool> SaveKpiList(List<KPIList> kpiList, CancellationToken cancellation = default);
}
