using MediatR;
using NiotechoneCQRS.Application.DTOs.ResponseDTOs;
using NiotechoneCQRS.Domain.Interfaces;
using NiotechoneCQRS.Utility.AppResource;
using System.Net;

namespace NiotechoneCQRS.Application.Company.Queries.GetCompanyById;

public record GetCompanyByIdQuery : IRequest<ResponseDTO<Domain.Entities.Company>>
{
    public int Id { get; set; }
}

public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, ResponseDTO<Domain.Entities.Company>>
{
    private readonly ICompanyRepository _companyRepository;

    public GetCompanyByIdQueryHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<ResponseDTO<Domain.Entities.Company>> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
    {

        var company = await _companyRepository.GetCompanyById(request.Id, cancellationToken);
        if (company == null)
        {
            return ResponseDTO<Domain.Entities.Company>.Failure(
                string.Format(AppResource.Notfound, "Company"),
                (int)HttpStatusCode.NotFound
            );
        }

        return ResponseDTO<Domain.Entities.Company>.Success(company, (int)HttpStatusCode.OK);
    }
}

