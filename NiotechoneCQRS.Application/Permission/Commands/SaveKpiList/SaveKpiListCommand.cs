using MediatR;
using NiotechoneCQRS.Application.DTOs.ResponseDTOs;
using NiotechoneCQRS.Domain.Entities;
using NiotechoneCQRS.Domain.Interfaces;
using System.Net;

namespace NiotechoneCQRS.Application.Permission.Commands.SaveKpiList;

public record SaveKpiListCommand(List<KPIList> kpiLists) : IRequest<ResponseDTO<bool>>
{
}

public class SaveKpiListCommandHandler : IRequestHandler<SaveKpiListCommand, ResponseDTO<bool>>
{
    private readonly IPermissionRepository _permissionRepository;
    public SaveKpiListCommandHandler(IPermissionRepository permissionRepository)
    {
        _permissionRepository = permissionRepository;
    }

    public async Task<ResponseDTO<bool>> Handle(SaveKpiListCommand command, CancellationToken cancellation)
    {
        var isSaved = await _permissionRepository.SaveKpiList(command.kpiLists);

        if (!isSaved)
        {
            return ResponseDTO<bool>.Failure(
                "Kpi list not saved sucessfully",
                (int)HttpStatusCode.BadRequest
            );
        }

        return ResponseDTO<bool>.Success(true, (int)HttpStatusCode.Created);
    }
}
