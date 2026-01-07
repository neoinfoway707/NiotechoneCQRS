using Dapper;
using NiotechoneCQRS.Domain.Entities;
using NiotechoneCQRS.Domain.Interfaces;
using NiotechoneCQRS.Infrastructure.Persistence.Data;

namespace NiotechoneCQRS.Infrastructure.Persistence.Repositories;

public class CountryRepository : ICountryRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    public CountryRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IList<Country>> GetAllCountries(CancellationToken cancellationToken = default)
    {
        using var conn = _connectionFactory.CreateConnection();

        const string sql = @"
                SELECT *
                FROM [Country]"
        ;

        var countries = await conn.QueryAsync<Country>(sql);

        return countries.AsList();
    }

    public async Task<IList<ParameterValue>> GetAllCurrency(CancellationToken cancellation = default)
    {
        using var conn = _connectionFactory.CreateConnection();

        const string sql = @"
            SELECT PV.*
            FROM ParameterValues PV
            INNER JOIN Parameter PR
                ON PV.ParameterId = PR.ParameterId
            WHERE PR.ParamName = @ParamName"
            ;

        var result = await conn.QueryAsync<ParameterValue>(
            sql,
            new { ParamName = "CURRENCY" }
        );

        return result.AsList();
    }

    public async Task<IList<ParameterValue>> GetLanguageList(CancellationToken cancellation = default)
    {
        using var conn = _connectionFactory.CreateConnection();

        const string sql = @"
            SELECT PV.*
            FROM ParameterValues PV
            INNER JOIN Parameter PR
                ON PV.ParameterId = PR.ParameterId
            WHERE PR.ParamName = @ParamName"
            ;

        var result = await conn.QueryAsync<ParameterValue>(
            sql,
            new { ParamName = "LANGUAGE" }
        );

        return result.AsList();
    }
}
