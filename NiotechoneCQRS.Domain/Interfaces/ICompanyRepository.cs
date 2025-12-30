using NiotechoneCQRS.Domain.Entities;

namespace NiotechoneCQRS.Domain.Interfaces;

public interface ICompanyRepository
{
    Task<IList<Company>> GetAllCompanies(CancellationToken cancellationToken = default);
}
