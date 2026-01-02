using Dapper;
using NiotechoneCQRS.Domain.Entities;
using NiotechoneCQRS.Domain.Interfaces;
using NiotechoneCQRS.Infrastructure.Persistence.Data;

namespace NiotechoneCQRS.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public UserRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IList<User>> GetAllUsers(CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();

        const string sql = @"
                SELECT *
                FROM [User] order by 1 desc";

        var users = await connection.QueryAsync<User>(sql);

        return users.AsList();
    }

    public async Task<User?> GetUserByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        const string sql = @"
                SELECT u.*,c.CompanyName
                FROM [User] u
                LEFT JOIN dbo.Company c ON u.CompanyId = c.CompanyId
                WHERE UserId = @UserId;
            ";

        using var connection = _connectionFactory.CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<User>(
            sql,
            new { UserId = id }
        );
    }

    public async Task<bool> DeleteUserByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        const string sql = @"
                DELETE FROM [User]
                WHERE UserId = @UserId;
            ";
        using var connection = _connectionFactory.CreateConnection();
        var rowsAffected = await connection.ExecuteAsync(
            sql,
            new { UserId = id }
        );
        return rowsAffected > 0;
    }

    public async Task<bool> CreateUserAsync(User user, CancellationToken cancellationToken = default)
    {
        const string sql = @"
                INSERT INTO [User] (FullName, UserRoleId, Address, CountryId, Email, Phone, Description, UserName, PasswordDecrypt,StatusId,CompanyId,UserTypeId)
                VALUES (@FullName, @UserRoleId, @Address, @CountryId, @Email, @Phone, @Description, @UserName, @PasswordDecrypt,@StatusId,@CompanyId,@UserTypeId);
            ";
        using var connection = _connectionFactory.CreateConnection();
        var rowsAffected = await connection.ExecuteAsync(
            sql,
            new
            {
                FullName = user.FullName,
                UserRoleId = user.UserRoleId,
                Address = user.Address,
                CountryId = user.CountryId,
                Email = user.Email,
                Phone = user.Phone,
                Description = user.Description,
                UserName = user.UserName,
                PasswordDecrypt = user.Password,
                StatusId = user.StatusId,
                CompanyId = user.CompanyId,
                UserTypeId = user.UserTypeId
            }
        );
        return rowsAffected > 0;
    }
    public async Task<bool> UpdateUserAsync(User user, CancellationToken cancellationToken)
    {
        try
        {
            var sql = @"
        UPDATE [dbo].[User]
        SET
            FullName = @FullName,
            UserRoleId = @UserRoleId,
            CompanyId = @CompanyId,
            Address = @Address,
            CountryId = @CountryId,
            Email = @Email,
            Phone = @Phone,
            Description = @Description,
            UserName = @UserName,
            StatusId = @StatusId,
            UserTypeId = @UserTypeId
            /**PASSWORD**/
        WHERE UserId = @UserId;";

            var parameters = new DynamicParameters(user); // Maps properties automatically if names match

            if (!string.IsNullOrWhiteSpace(user.PasswordDecrypt))
            {
                sql = sql.Replace("/**PASSWORD**/", ", PasswordDecrypt = @Password");
                parameters.Add("Password", user.PasswordDecrypt);
            }
            else
            {
                sql = sql.Replace("/**PASSWORD**/", "");
            }

            using var connection = _connectionFactory.CreateConnection();
            var rowsAffected = await connection.ExecuteAsync(
                new CommandDefinition(
                    sql,
                    parameters,
                    cancellationToken: cancellationToken
                )
            );
            return rowsAffected > 0;
        }
        catch (Exception ex)
        {

            throw;
        }
        
    }
    public async Task<User?> IsUserValid(string email, string password, int? companyId)
    {
        const string sql = @"
    SELECT TOP 1 u.*, c.CompanyName
    FROM dbo.[User] u
    LEFT JOIN dbo.Company c ON u.CompanyId = c.CompanyId
    WHERE u.Email = @Email
      AND u.PasswordDecrypt = @PasswordDecrypt
      AND (@CompanyId IS NULL OR u.CompanyId = @CompanyId);
";

        using var connection = _connectionFactory.CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<User>(
            sql,
            new
            {
                Email = email,
                PasswordDecrypt = password,
                CompanyId = companyId
            });
    }




    public async Task<bool> UpdateUserTokenAsync(long userId, string token, CancellationToken cancellationToken)
    {
        var sql = @"
        UPDATE [dbo].[User]
        SET
            token = @token
        WHERE UserId = @UserId;";

        var parameters = new { UserId = userId, token = token };

        using var connection = _connectionFactory.CreateConnection();
        var rowsAffected = await connection.ExecuteAsync(
            new CommandDefinition(
                sql,
                parameters,
                cancellationToken: cancellationToken
            )
        );
        return rowsAffected > 0;
    }
}