using Dapper;
using NiotechoneCQRS.Domain.Entities;
using NiotechoneCQRS.Domain.Interfaces;
using NiotechoneCQRS.Infrastructure.Persistence.Data;

namespace NiotechoneCQRS.Infrastructure.Persistence.Repositories;

public class UserRoleRepository : IUserRoleRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public UserRoleRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IList<UserRole>> GetAllUserRoles(CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();

        const string sql = @"
                SELECT *
                FROM UserRole order by 1 desc";

        var userRoles = await connection.QueryAsync<UserRole>(sql);

        return userRoles.AsList();
    }
}
