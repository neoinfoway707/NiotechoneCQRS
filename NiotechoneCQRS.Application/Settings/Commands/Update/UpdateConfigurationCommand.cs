using AutoMapper;
using MediatR;
using NiotechoneCQRS.Application.DTOs.ResponseDTOs;
using NiotechoneCQRS.Domain.Entities;
using NiotechoneCQRS.Domain.Interfaces;
using NiotechoneCQRS.Infrastructure.Persistence.Repositories;
using NiotechoneCQRS.Utility.CommonResource;
using System.Net;


namespace NiotechoneCQRS.Application.Settings.Commands.Update;

public class UpdateConfigurationCommand : IRequest<ResponseDTO<bool>>
{
    public int ConfigManagerValueId { get; set; }
    public string Value { get; set; }
    public string Remarks { get; set; }
}

public class UpdateConfigurationCommandHandler : IRequestHandler<UpdateConfigurationCommand, ResponseDTO<bool>>
{
    private readonly ISettingRepository _settingRepository;
    public UpdateConfigurationCommandHandler(ISettingRepository settingRepository)
    {
        _settingRepository = settingRepository;
    }
    public async Task<ResponseDTO<bool>> Handle(UpdateConfigurationCommand request, CancellationToken cancellationToken)
    {

        var valueEntity = new ConfigManagerValue
        {
            ConfigManagerValueId = request.ConfigManagerValueId,
            Value = request.Value,
            Remarks = request.Remarks
        };

        var updated = await _settingRepository.UpdateConfiguration(valueEntity, cancellationToken);

        if (!updated)
        {
            return ResponseDTO<bool>.Failure(
                "Configuration not found or update failed",
                (int)HttpStatusCode.NotFound
            );
        }

        return ResponseDTO<bool>.Success(
          true,
          (int)HttpStatusCode.OK
      );
    }
}