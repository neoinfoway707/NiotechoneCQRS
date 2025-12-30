using Dapper;
using NiotechoneCQRS.Domain.Entities;
using NiotechoneCQRS.Domain.Interfaces;
using NiotechoneCQRS.Infrastructure.Persistence.Data;

namespace NiotechoneCQRS.Infrastructure.Persistence.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    public CompanyRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IList<Company>> GetAllCompanies(CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();

        const string sql = @"
                SELECT *
                FROM [Company]";

        var companies = await connection.QueryAsync<Company>(sql);

        return companies.AsList();
    }
}
