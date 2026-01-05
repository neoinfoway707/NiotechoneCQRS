using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using NiotechoneCQRS.Application.DTOs.ResponseDTOs;
using NiotechoneCQRS.Domain.Entities;
using NiotechoneCQRS.Domain.Interfaces;
using System.Net;
using System.Text.Json.Serialization;

namespace NiotechoneCQRS.Application.Company.Commands.Update;

public class UpdateCompanyCommand : IRequest<ResponseDTO<bool>>
{
    [JsonIgnore]
    public int Id { get; set; }
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

    public CompanyLogoUpdate? LogoFile { get; set; }
    public CompanyHeaderUpload? HeaderFile { get; set; }
    public CompanyFooterUpload? FooterFile { get; set; }
}

public class CompanyLogoUpdate
{
    public byte[] LogoContent { get; set; } = null!;
    public string LogoFileName { get; set; } = null!;
    public string LogoContentType { get; set; } = null!;
}

public class CompanyHeaderUpload
{
    public byte[] HeaderContent { get; set; } = null!;
    public string HeaderFileName { get; set; } = null!;
    public string HeaderContentType { get; set; } = null!;
}

public class CompanyFooterUpload
{
    public byte[] FooterContent { get; set; } = null!;
    public string FooterFileName { get; set; } = null!;
    public string FooterContentType { get; set; } = null!;
}

public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, ResponseDTO<bool>>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateCompanyCommandHandler(ICompanyRepository companyRepository, IHttpContextAccessor httpContextAccessor)
    {
        _companyRepository = companyRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ResponseDTO<bool>> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        var userId = long.Parse(_httpContextAccessor.HttpContext!.User.FindFirst("UserId")!.Value);

        var company = new Domain.Entities.Company
        {
            CompanyId = request.Id,
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
                FileName = request.LogoFile.LogoFileName,
                CompanyImage = request.LogoFile.LogoContent,
                ContentType = request.LogoFile.LogoContentType
            };
        }

        var artifacts = new List<CompanyArtifact>();

        if (request.HeaderFile != null)
        {
            artifacts.Add(new CompanyArtifact
            {
                Filename = request.HeaderFile.HeaderFileName,
                CompanyImage = request.HeaderFile.HeaderContent,
                ContentType = request.HeaderFile.HeaderContentType,
                ArtifactType = (int)Enum.Enums.ArtifactType.CompanyHeader,
                UpdatedBy = userId
            });
        }

        if (request.FooterFile != null)
        {
            artifacts.Add(new CompanyArtifact
            {
                Filename = request.FooterFile.FooterFileName,
                CompanyImage = request.FooterFile.FooterContent,
                ContentType = request.FooterFile.FooterContentType,
                ArtifactType = (int)Enum.Enums.ArtifactType.CompanyFooter,
                UpdatedBy = userId
            });
        }

        var updated = await _companyRepository.UpdateCompany(
            company,
            logo,
            artifacts.Any() ? artifacts : null,
            cancellationToken
        );

        if (!updated)
        {
            return ResponseDTO<bool>.Failure(
                "Company not found or update failed",
                (int)HttpStatusCode.NotFound
            );
        }

        return ResponseDTO<bool>.Success(
            true,
            (int)HttpStatusCode.OK
        );
    }
}
