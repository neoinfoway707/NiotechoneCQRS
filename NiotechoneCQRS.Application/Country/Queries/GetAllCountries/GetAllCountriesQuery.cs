using MediatR;
using NiotechoneCQRS.Application.DTOs.ResponseDTOs;
using NiotechoneCQRS.Domain.Interfaces;
using NiotechoneCQRS.Utility.CommonResource;

namespace NiotechoneCQRS.Application.Country.Queries.GetAllCountries;

public class GetAllCountriesQuery : IRequest<ResponseDTO<IList<Domain.Entities.Country>>>
{
}

public class GetAllCountriesQueryHandler : IRequestHandler<GetAllCountriesQuery, ResponseDTO<IList<Domain.Entities.Country>>>
{
    private readonly ICountryRepository _countryRepository;
    public GetAllCountriesQueryHandler(ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;
    }

    public async Task<ResponseDTO<IList<Domain.Entities.Country>>> Handle(GetAllCountriesQuery query, CancellationToken cancellationToken)
    {
        var countries = await _countryRepository
            .GetAllCountries(cancellationToken);

        if (countries == null || !countries.Any())
        {
            return ResponseDTO<IList<Domain.Entities.Country>>.Failure(
                string.Format(AppResource.Notfound, "Countries"),
                404
            );
        }

        return ResponseDTO<IList<Domain.Entities.Country>>.Success(countries, 200);
    }
}
