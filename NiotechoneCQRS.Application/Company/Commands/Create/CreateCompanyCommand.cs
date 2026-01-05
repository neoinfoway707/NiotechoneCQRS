using AutoMapper;
using MediatR;
using NiotechoneCQRS.Application.DTOs.ResponseDTOs;
using NiotechoneCQRS.Domain.Entities;
using NiotechoneCQRS.Domain.Interfaces;
using System.Net;

namespace NiotechoneCQRS.Application.Company.Commands.Create;

public record CreateCompanyCommand : IRequest<ResponseDTO<bool>>
{
    public string CompanyName { get; set; } = null!;
    public string? Address { get; set; }
    public string? City { get; set; }
    public int CountryId { get; set; }
    public string TimeZone { get; set; } = null!;
    public string? POBox { get; set; }
    public string? Phone { get; set; }

    public int StatusId { get; set; }
    public bool? Billable { get; set; }
    public bool? WorkRequest { get; set; }
    public string? WorkRequestURL { get; set; }

    public int CurrencyId { get; set; }
    public int? ThresholdValue { get; set; }
    public decimal VAT { get; set; }
    public string? TaxRegistrationNo { get; set; }

    public int? LanguageId { get; set; }
    public string? PurchaseReqEmails { get; set; }

    public CompanyLogoUpload? LogoFile { get; set; }
}

public class CompanyLogoUpload
{
    public byte[] Content { get; set; } = null!;
    public string FileName { get; set; } = null!;
    public string ContentType { get; set; } = null!;
}

public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, ResponseDTO<bool>>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public CreateCompanyCommandHandler(ICompanyRepository companyRepository, IMapper mapper)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
    }

    public async Task<ResponseDTO<bool>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = new Domain.Entities.Company
        {
            CompanyName = request.CompanyName,
            Address = request.Address ?? string.Empty,
            City = request.City ?? string.Empty,
            CountryId = request.CountryId,
            Timezone = request.TimeZone,
            POBox = request.POBox ?? string.Empty,
            Phone = request.Phone ?? string.Empty,

            StatusId = request.StatusId,
            Billable = request.Billable,
            WorkRequest = request.WorkRequest,
            WorkRequestURL = request.WorkRequestURL ?? string.Empty,

            CurrencyId = request.CurrencyId,
            ThresholdValue = request.ThresholdValue,
            VAT = request.VAT,
            TaxRegistrationNo = request.TaxRegistrationNo ?? string.Empty,

            LanguageId = request.LanguageId,
            PurchaseReqEmails = request.PurchaseReqEmails ?? string.Empty
        };

        CompanyLogo? logo = null;

        if (request.LogoFile != null)
        {
            logo = new CompanyLogo
            {
                FileName = request.LogoFile.FileName,
                CompanyImage = request.LogoFile.Content,
                ContentType = request.LogoFile.ContentType
            };
        }

        var result = await _companyRepository.CreateCompany(
            company,
            logo,
            cancellationToken
        );

        if (!result)
        {
            return ResponseDTO<bool>.Failure(
                "Company creation failed",
                (int)HttpStatusCode.BadRequest
            );
        }

        return ResponseDTO<bool>.Success(
            true,
            (int)HttpStatusCode.Created
        );
    }
}
