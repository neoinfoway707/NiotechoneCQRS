using NiotechoneCQRS.Domain.Entities;

namespace NiotechoneCQRS.Domain.Interfaces;

public interface ICountryRepository
{
    Task<IList<Country>> GetAllCountries(CancellationToken cancellation = default);
    Task<IList<ParameterValue>> GetAllCurrency(CancellationToken cancellation = default);
    Task<IList<ParameterValue>> GetLanguageList(CancellationToken cancellation = default);
}
