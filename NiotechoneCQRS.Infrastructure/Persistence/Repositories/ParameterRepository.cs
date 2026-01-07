using Dapper;
using NiotechoneCQRS.Domain.Entities;
using NiotechoneCQRS.Domain.Interfaces;
using NiotechoneCQRS.Infrastructure.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiotechoneCQRS.Infrastructure.Persistence.Repositories;

public class ParameterRepository : IParameterRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public ParameterRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<List<ParameterValue>> GetParameterValues(
        string parameterName,
        CancellationToken cancellationToken)
    {
        const string sql = @"
            SELECT 
                pv.ParameterValuesId,
                pv.ParameterId,
                pv.Value,
                pv.Name,
                pv.Description
            FROM Parameter p
            INNER JOIN ParameterValues pv
                ON p.ParameterId = pv.ParameterId
            WHERE p.ParamName = @ParameterName;
        ";

        using var connection = _connectionFactory.CreateConnection();

        var result = await connection.QueryAsync<ParameterValue>(
            sql,
            new { ParameterName = parameterName }
        );

        return result.ToList();
    }
}
