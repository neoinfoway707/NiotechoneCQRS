using MediatR;
using NiotechoneCQRS.Application.DTOs.ResponseDTOs;
using NiotechoneCQRS.Domain.Entities;
using NiotechoneCQRS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiotechoneCQRS.Application.Settings.Queries.GetConfigMasterByKey;

public record GetConfigMasterByKeyQuery(string ConfigMasterKey, long CompanyId) : IRequest<ResponseDTO<ConfigManagerValue>>;

public class GetConfigMasterByKeyQueryHandler
    : IRequestHandler<GetConfigMasterByKeyQuery, ResponseDTO<ConfigManagerValue>>
{
    private readonly ISettingRepository _settingRepository;

    public GetConfigMasterByKeyQueryHandler(ISettingRepository settingRepository)
    {
        _settingRepository = settingRepository;
    }

    public async Task<ResponseDTO<ConfigManagerValue>> Handle(
        GetConfigMasterByKeyQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _settingRepository.GetConfigMasterByKey(
            request.ConfigMasterKey,
            request.CompanyId,
            cancellationToken
        );

        return ResponseDTO<ConfigManagerValue>.Success(result);
    }
}