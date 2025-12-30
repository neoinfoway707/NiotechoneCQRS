using MediatR;
using NiotechoneCQRS.Application.DTOs.ResponseDTOs;
using NiotechoneCQRS.Domain.Interfaces;
using NiotechoneCQRS.Utility.AppResource;

namespace NiotechoneCQRS.Application.Company.Queries.GetAllCompanies;

public class GetAllCompaniesQuery
    : IRequest<ResponseDTO<IList<Domain.Entities.Company>>>
{
}

public class GetAllCompaniesQueryHandler
    : IRequestHandler<GetAllCompaniesQuery, ResponseDTO<IList<Domain.Entities.Company>>>
{
    private readonly ICompanyRepository _companyRepository;

    public GetAllCompaniesQueryHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<ResponseDTO<IList<Domain.Entities.Company>>> Handle(
        GetAllCompaniesQuery request,
        CancellationToken cancellationToken)
    {
        var companies = await _companyRepository.GetAllCompanies(cancellationToken);

        if (companies == null || !companies.Any())
        {
            return ResponseDTO<IList<Domain.Entities.Company>>.Failure(
                string.Format(AppResource.Notfound, "Companies"),
                404
            );
        }

        return ResponseDTO<IList<Domain.Entities.Company>>.Success(companies, 200);
    }
}
