using AutoMapper;
using MediatR;
using NiotechoneCQRS.Application.DTOs.RequestDTOs;
using NiotechoneCQRS.Application.DTOs.ResponseDTOs;
using NiotechoneCQRS.Application.User.Commands.Create;
using NiotechoneCQRS.Domain.Entities;
using NiotechoneCQRS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NiotechoneCQRS.Application.Settings.Commands.SaveConfiguration;

public record SaveSystemConfigurationCommand(IList<ConfigManagerValueRequestDTO> configManagerValueRequestDTO) : IRequest<ResponseDTO<bool>>
{
}

public class SaveSystemConfigurationHandler : IRequestHandler<SaveSystemConfigurationCommand, ResponseDTO<bool>>
{
    private readonly ISettingRepository _settingRepository;
    public SaveSystemConfigurationHandler(ISettingRepository settingRepository)
    {
        _settingRepository = settingRepository;
    }
    public async Task<ResponseDTO<bool>> Handle(SaveSystemConfigurationCommand request, CancellationToken cancellationToken)
    {
        var configValues = new List<ConfigManagerValue>();
        configValues = request.configManagerValueRequestDTO.Select(dto => new ConfigManagerValue
        {
            ConfigManagerValueId = dto.ConfigManagerValueId,
            ConfigManagerKeyId = dto.ConfigManagerKeyId,
            CompanyId = dto.CompanyId,
            Value = dto.Value,
            Remarks = dto.Remarks
        }).ToList();

        var result = await _settingRepository.SaveSystemConfigurationValue(
           configValues,
           cancellationToken
         );
        return ResponseDTO<bool>.Success(result);
    }
}