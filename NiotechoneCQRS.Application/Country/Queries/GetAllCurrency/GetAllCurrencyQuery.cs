using MediatR;
using NiotechoneCQRS.Application.DTOs.ResponseDTOs;
using NiotechoneCQRS.Domain.Entities;
using NiotechoneCQRS.Domain.Interfaces;
using NiotechoneCQRS.Utility.CommonResource;

namespace NiotechoneCQRS.Application.Country.Queries.GetAllCurrency;

public class GetAllCurrencyQuery : IRequest<ResponseDTO<IList<ParameterValue>>>
{
}

public class GetAllCurrencyQueryHandler
    : IRequestHandler<GetAllCurrencyQuery, ResponseDTO<IList<ParameterValue>>>
{
    private readonly ICountryRepository _countryRepository;

    public GetAllCurrencyQueryHandler(ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;
    }

    public async Task<ResponseDTO<IList<ParameterValue>>> Handle(
        GetAllCurrencyQuery request,
        CancellationToken cancellationToken)
    {
        var currency = await _countryRepository.GetAllCurrency(cancellationToken);

        if (currency == null || !currency.Any())
        {
            return ResponseDTO<IList<ParameterValue>>.Failure(
                string.Format(AppResource.Notfound, "Currency"),
                404
            );
        }

        return ResponseDTO<IList<ParameterValue>>.Success(currency, 200);
    }
}
