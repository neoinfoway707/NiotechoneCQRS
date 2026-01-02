using MediatR;
using NiotechoneCQRS.Application.DTOs.ResponseDTOs;
using NiotechoneCQRS.Domain.Interfaces;
using NiotechoneCQRS.Utility.AppResource;
using System.Net;

namespace NiotechoneCQRS.Application.Company.Commands.Delete;

public record DeleteCompanyCommand : IRequest<ResponseDTO<bool>>
{
    public int Id { get; set; }
}

public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, ResponseDTO<bool>>
{
    private readonly ICompanyRepository _companyRepository;
    public DeleteCompanyCommandHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<ResponseDTO<bool>> Handle(DeleteCompanyCommand command, CancellationToken cancellationToken)
    {
        var isDeleted = await _companyRepository
            .DeleteCompany(command.Id, cancellationToken);

        if (!isDeleted)
        {
            return ResponseDTO<bool>.Failure(
                string.Format(AppResource.Notfound, "Company"),
                (int)HttpStatusCode.NotFound
            );
        }
        return ResponseDTO<bool>.Success(isDeleted, (int)HttpStatusCode.OK);
    }
}
