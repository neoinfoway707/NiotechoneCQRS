using Dapper;
using NiotechoneCQRS.Domain.Enum;
using NiotechoneCQRS.Domain.Entities;
using NiotechoneCQRS.Domain.Interfaces;
using NiotechoneCQRS.Infrastructure.Persistence.Data;
using System.Data;

namespace NiotechoneCQRS.Infrastructure.Persistence.Repositories;

public class SettingRepository : ISettingRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public SettingRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IList<dynamic>> GetConfigurationList(long CompanyId, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();

        const string sql = @"
            SELECT
                ck.ConfigManagerKeyId   AS ConfigManagerId,
                cv.ConfigManagerValueId AS ConfigManagerValueId,
                c.CompanyId,
                ck.Category,
                ck.[Key],
                ISNULL(cv.[Value], '')  AS [Value],
                ISNULL(cv.Remarks, '')  AS Remarks
            FROM Company c
            CROSS JOIN ConfigManagerKey ck
            LEFT JOIN ConfigManagerValue cv
                ON cv.ConfigManagerKeyId = ck.ConfigManagerKeyId
               AND cv.CompanyId = c.CompanyId
            WHERE c.StatusId = 1
              AND c.CompanyId = @CompanyId
            ORDER BY c.CompanyId, ck.ConfigManagerKeyId;
        ";

        var data = await connection.QueryAsync(sql, new { CompanyId });

        return data.AsList();
    }

    public async Task<bool> UpdateConfiguration(ConfigManagerValue value, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction();

        try
        {
            var updateValueSql = @"
                UPDATE ConfigManagerValue
                SET 
                    [Value] = @Value,
                    Remarks = @Remarks
                WHERE ConfigManagerValueId = @ConfigManagerValueId;
            ";

            var rowsAffected = await connection.ExecuteAsync(updateValueSql, new
            {
                value.ConfigManagerValueId,
                value.Value,
                value.Remarks
            }, transaction);

            transaction.Commit();
            return rowsAffected > 0;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task<bool> SaveSystemConfigurationValue(
     IList<ConfigManagerValue> configList,
     CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();

        using var transaction = connection.BeginTransaction();

        try
        {
            foreach (var item in configList)
            {
                if (item.ConfigManagerValueId == 0)
                {
                    // INSERT (FIXED)
                    const string insertSql = @"
                    INSERT INTO ConfigManagerValue
                    (ConfigManagerKeyId, CompanyId, [Value], Remarks)
                    VALUES
                    (@ConfigManagerKeyId, @CompanyId, @Value, @Remarks);
                ";

                    await connection.ExecuteAsync(
                        insertSql,
                        new
                        {
                            item.ConfigManagerKeyId,
                            item.CompanyId,
                            item.Value,
                            item.Remarks
                        },
                        transaction
                    );
                }
                else
                {
                    // GET EXISTING VALUE (OLD EF BEHAVIOR)
                    const string selectSql = @"
                    SELECT [Value]
                    FROM ConfigManagerValue
                    WHERE ConfigManagerValueId = @ConfigManagerValueId;
                ";

                    var existingValue = await connection.QueryFirstOrDefaultAsync<string>(
                        selectSql,
                        new { item.ConfigManagerValueId },
                        transaction
                    );

                    // UPDATE ONLY IF VALUE CHANGED
                    if (existingValue != item.Value)
                    {
                        const string updateSql = @"
                        UPDATE ConfigManagerValue
                        SET
                            [Value] = @Value,
                            Remarks = @Remarks
                        WHERE ConfigManagerValueId = @ConfigManagerValueId;
                    ";

                        await connection.ExecuteAsync(
                            updateSql,
                            new
                            {
                                item.Value,
                                item.Remarks,
                                item.ConfigManagerValueId
                            },
                            transaction
                        );

                        // APPLY MODULE PERMISSION LOGIC
                        if (item.Value == "0")
                        {
                            await DisableModulePermissions(
                                connection,
                                transaction,
                                item,
                                cancellationToken
                            );
                        }
                    }
                }
            }

            transaction.Commit();
            return true;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    private async Task DisableModulePermissions(
     IDbConnection connection,
     IDbTransaction transaction,
     ConfigManagerValue item,
     CancellationToken cancellationToken)
    {
        const string keySql = @"
        SELECT [Key]
        FROM ConfigManagerKey
        WHERE ConfigManagerKeyId = @ConfigManagerKeyId;
    ";

        var configKey = await connection.QueryFirstOrDefaultAsync<string>(
            keySql,
            new { item.ConfigManagerKeyId },
            transaction
        );

        if (configKey == null) return;

        string? moduleCode = configKey switch
        {
            nameof(Enums.ConfigMasterKey.Show_Client_Module_Config) => "CLI",
            nameof(Enums.ConfigMasterKey.Show_ClientContracts_ModuleConfig) => "CLI-CON",
            nameof(Enums.ConfigMasterKey.Show_SubContractorContracts_Config) => "PER-SC-CON",
            _ => null
        };

        if (moduleCode == null) return;

        const string sql = @"
        UPDATE urmo
        SET StatusId = 0
        FROM UserRoleModuleOperations urmo
        INNER JOIN UserRoles ur
            ON ur.UserRoleId = urmo.UserRoleId
        INNER JOIN Modules m
            ON m.ModuleId = urmo.ModuleId
        WHERE ur.CompanyId = @CompanyId
          AND m.ModuleCode = @ModuleCode;
    ";

        await connection.ExecuteAsync(
            sql,
            new
            {
                CompanyId = item.CompanyId,
                ModuleCode = moduleCode
            },
            transaction
        );
    }

    public async Task<ConfigManagerValue?> GetConfigMasterByKey(
        string configMasterKey,
        long companyId,
        CancellationToken cancellationToken)
    {
        const string sql = @"
            SELECT
                cv.ConfigManagerValueId,
                cv.ConfigManagerKeyId,
                cv.CompanyId,
                cv.[Value],
                cv.Remarks,
                ck.[Key],
                ck.Category
            FROM ConfigManagerValue cv
            INNER JOIN ConfigManagerKey ck
                ON ck.ConfigManagerKeyId = cv.ConfigManagerKeyId
            WHERE ck.[Key] = @ConfigMasterKey
              AND cv.CompanyId = @CompanyId;
        ";

        using var connection = _connectionFactory.CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<ConfigManagerValue>(
            sql,
            new
            {
                ConfigMasterKey = configMasterKey,
                CompanyId = companyId
            }
        );
    }

}
