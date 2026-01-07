using MediatR;
using NiotechoneCQRS.Application.DTOs.ResponseDTOs;
using NiotechoneCQRS.Domain.Entities;
using NiotechoneCQRS.Domain.Interfaces;
using NiotechoneCQRS.Utility.CommonResource;

namespace NiotechoneCQRS.Application.Country.Queries.GetLanguageList;

public class GetLanguageListQuery : IRequest<ResponseDTO<IList<ParameterValue>>>
{
}

public class GetLanguageListQueryHandler
    : IRequestHandler<GetLanguageListQuery, ResponseDTO<IList<ParameterValue>>>
{
    private readonly ICountryRepository _countryRepository;

    public GetLanguageListQueryHandler(ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;
    }

    public async Task<ResponseDTO<IList<ParameterValue>>> Handle(
        GetLanguageListQuery request,
        CancellationToken cancellationToken)
    {
        var languageList = await _countryRepository.GetLanguageList(cancellationToken);

        if (languageList == null || !languageList.Any())
        {
            return ResponseDTO<IList<ParameterValue>>.Failure(
                string.Format(AppResource.Notfound, "Language List"),
                404
            );
        }

        return ResponseDTO<IList<ParameterValue>>.Success(languageList, 200);
    }
}