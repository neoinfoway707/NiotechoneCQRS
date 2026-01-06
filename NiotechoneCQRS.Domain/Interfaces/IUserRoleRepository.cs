using NiotechoneCQRS.Domain.Entities;

namespace NiotechoneCQRS.Domain.Interfaces;

public interface IUserRoleRepository
{
    Task<IList<UserRole>> GetAllUserRoles(CancellationToken cancellationToken = default);
}
