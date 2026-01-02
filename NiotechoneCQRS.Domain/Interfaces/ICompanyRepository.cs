using NiotechoneCQRS.Domain.Entities;

namespace NiotechoneCQRS.Domain.Interfaces;

public interface ICompanyRepository
{
    Task<IList<Company>> GetAllCompanies(CancellationToken cancellationToken = default);
    Task<Company?> GetCompanyById(int id, CancellationToken cancellationToken = default);
    Task<bool> CreateCompany(Company company, CompanyLogo? companyLogo, CancellationToken cancellationToken = default);
    Task<bool> UpdateCompany(Company company, CompanyLogo? companyLogo, IList<CompanyArtifact>? companyArtifacts, CancellationToken cancellationToken = default);
    Task<bool> DeleteCompany(int id, CancellationToken cancellationToken = default);
}
