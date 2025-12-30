using NiotechoneCQRS.Domain.Entities;

namespace NiotechoneCQRS.Domain.Interfaces;

public interface IUserRepository
{
    Task<IList<User>> GetAllUsers(CancellationToken cancellationToken = default);
    Task<User> GetUserByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<bool> DeleteUserByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<bool> CreateUserAsync(User user, CancellationToken cancellationToken = default);
    Task<bool> UpdateUserAsync(User user, CancellationToken cancellationToken = default);
    Task<User?> IsUserValid(string email, string password, int? companyId);
    Task<bool> UpdateUserTokenAsync(long userId, string token, CancellationToken cancellationToken = default);
}
