using Dapper;
using NiotechoneCQRS.Domain.Entities;
using NiotechoneCQRS.Domain.Interfaces;
using NiotechoneCQRS.Infrastructure.Persistence.Data;

namespace NiotechoneCQRS.Infrastructure.Persistence.Repositories;

public class ConfigManagerRepository : IConfigManagerRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public ConfigManagerRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<ConfigManagerValue?> GetConfigMasterByKey(string configMasterKey, long companyId)
    {
        using var connection = _connectionFactory.CreateConnection();

        var sql = @"
            SELECT TOP 1 cmv.*
            FROM ConfigManagerValue cmv
            INNER JOIN ConfigManagerKey cmk
                ON cmk.ConfigManagerKeyId = cmv.ConfigManagerKeyId
            WHERE cmk.[Key] = @ConfigMasterKey
              AND cmv.CompanyId = @CompanyId";

        var configEntity = await connection.QueryFirstOrDefaultAsync<ConfigManagerValue>(
            sql,
            new
            {
                ConfigMasterKey = configMasterKey,
                CompanyId = companyId
            }
        );

        return configEntity;
    }
}
