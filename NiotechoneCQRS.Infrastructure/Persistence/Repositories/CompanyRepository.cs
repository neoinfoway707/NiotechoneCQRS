using Dapper;
using Microsoft.EntityFrameworkCore;
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

    public async Task<Company?> GetCompanyById(long id, CancellationToken cancellationToken = default)
    {
        const string sql = @"
                SELECT *
                FROM [Company]
                WHERE CompanyId = @CompanyId;
            ";

        using var connection = _connectionFactory.CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<Company>(
            sql,
            new { CompanyId = id }
        );
    }

    public async Task<bool> CreateCompany(Company company, CompanyLogo? companyLogo, CancellationToken cancellationToken = default)
    {
        const string insertCompanySql = @"
            INSERT INTO Company
            (
                CompanyName,
                Address,
                City,
                CountryId,
                TimeZone,
                POBox,
                Phone,
                StatusId,
                Billable,
                WorkRequest,
                WorkRequestURL,
                CurrencyId,
                ThresholdValue,
                VAT,
                TaxRegistrationNo,
                LanguageId,
                PurchaseReqEmails
            )
            VALUES
            (
                @CompanyName,
                @Address,
                @City,
                @CountryId,
                @TimeZone,
                @POBox,
                @Phone,
                @StatusId,
                @Billable,
                @WorkRequest,
                @WorkRequestURL,
                @CurrencyId,
                @ThresholdValue,
                @VAT,
                @TaxRegistrationNo,
                @LanguageId,
                @PurchaseReqEmails
            );

            SELECT CAST(SCOPE_IDENTITY() AS INT);
        ";

        const string insertLogoSql = @"
            INSERT INTO CompanyLogo
            (
                CompanyId,
                FileName,
                CompanyImage,
                ContentType
            )
            VALUES
            (
                @CompanyId,
                @FileName,
                @CompanyImage,
                @ContentType
            );
        ";

        using var connection = _connectionFactory.CreateConnection();
        connection.Open();
        
        using var transaction = connection.BeginTransaction();

        try
        {
            // Insert Company
            var companyId = await connection.ExecuteScalarAsync<int>(
                insertCompanySql,
                company,
                transaction
            );

            // Insert Logo (if exists)
            if (companyLogo is not null)
            {
                await connection.ExecuteAsync(
                    insertLogoSql,
                    new
                    {
                        CompanyId = companyId,
                        companyLogo.FileName,
                        companyLogo.CompanyImage,
                        companyLogo.ContentType
                    },
                    transaction
                );
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

    public async Task<bool> UpdateCompany(Company company, CompanyLogo? companyLogo, IList<CompanyArtifact>? companyArtifacts, CancellationToken cancellationToken = default)
    {
        const string updateCompanySql = @"
            UPDATE Company
            SET
                CompanyName = @CompanyName,
                Address = @Address,
                City = @City,
                CountryId = @CountryId,
                TimeZone = @TimeZone,
                POBox = @POBox,
                Phone = @Phone,
                StatusId = @StatusId,
                Billable = @Billable,
                WorkRequest = @WorkRequest,
                WorkRequestURL = @WorkRequestURL,
                CurrencyId = @CurrencyId,
                ThresholdValue = @ThresholdValue,
                VAT = @VAT,
                TaxRegistrationNo = @TaxRegistrationNo,
                LanguageId = @LanguageId,
                PurchaseReqEmails = @PurchaseReqEmails
            WHERE CompanyId = @CompanyId;
        ";

        const string upsertCompanyLogoSql = @"
            IF EXISTS (SELECT 1 FROM CompanyLogo WHERE CompanyId = @CompanyId)
            BEGIN
                UPDATE CompanyLogo
                SET
                    FileName = @FileName,
                    CompanyImage = @CompanyImage,
                    ContentType = @ContentType,
                    UpdatedDate = SYSUTCDATETIME()
                WHERE CompanyId = @CompanyId;
            END
            ELSE
            BEGIN
                INSERT INTO CompanyLogo
                (
                    CompanyId,
                    FileName,
                    CompanyImage,
                    ContentType,
                    UpdatedDate
                )
                VALUES
                (
                    @CompanyId,
                    @FileName,
                    @CompanyImage,
                    @ContentType,
                    SYSUTCDATETIME()
                );
            END
        ";

        const string upsertArtifactSql = @"
            IF EXISTS (
                SELECT 1
                FROM CompanyArtifacts
                WHERE CompanyId = @CompanyId
                  AND ArtifactType = @ArtifactType
            )
            BEGIN
                UPDATE CompanyArtifacts
                SET
                    FileName = @Filename,
                    CompanyImage = @CompanyImage,
                    ContentType = @ContentType,
                    UpdatedDate = SYSUTCDATETIME(),
                    UpdatedBy = @UpdatedBy
                WHERE CompanyId = @CompanyId
                  AND ArtifactType = @ArtifactType;
            END
            ELSE
            BEGIN
                INSERT INTO CompanyArtifacts
                (
                    CompanyId,
                    FileName,
                    CompanyImage,
                    ContentType,
                    ArtifactType,
                    UpdatedDate,
                    UpdatedBy
                )
                VALUES
                (
                    @CompanyId,
                    @Filename,
                    @CompanyImage,
                    @ContentType,
                    @ArtifactType,
                    SYSUTCDATETIME(),
                    @UpdatedBy
                );
            END
        ";

        using var connection = _connectionFactory.CreateConnection();
        connection.Open();

        using var transaction = connection.BeginTransaction();

        try
        {
            var rowsAffected = await connection.ExecuteAsync(
                updateCompanySql,
                company,
                transaction
            );

            if (rowsAffected == 0)
            {
                transaction.Rollback();
                return false;
            }

            if (companyLogo != null)
            {
                await connection.ExecuteAsync(
                    upsertCompanyLogoSql,
                    new
                    {
                        CompanyId = company.CompanyId,
                        companyLogo.FileName,
                        companyLogo.CompanyImage,
                        companyLogo.ContentType
                    },
                    transaction
                );
            }

            if (companyArtifacts != null && companyArtifacts.Any())
            {
                foreach (var artifact in companyArtifacts)
                {
                    await connection.ExecuteAsync(
                        upsertArtifactSql,
                        new
                        {
                            CompanyId = company.CompanyId,
                            artifact.Filename,
                            artifact.CompanyImage,
                            artifact.ContentType,
                            artifact.ArtifactType,
                            artifact.UpdatedBy
                        },
                        transaction
                    );
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

    public async Task<bool> DeleteCompany(int id, CancellationToken cancellationToken = default)
    {
        const string sql = @"
                DELETE FROM Company
                WHERE CompanyId = @CompanyId;
            ";
        using var connection = _connectionFactory.CreateConnection();
        var rowsAffected = await connection.ExecuteAsync(
            sql,
            new { CompanyId = id }
        );
        return rowsAffected > 0;
    }
}
