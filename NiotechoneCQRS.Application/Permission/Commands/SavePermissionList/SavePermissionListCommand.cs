using MediatR;
using NiotechoneCQRS.Application.DTOs.ResponseDTOs;
using NiotechoneCQRS.Domain.Entities;
using NiotechoneCQRS.Domain.Interfaces;
using System.Net;

namespace NiotechoneCQRS.Application.Permission.Commands.SavePermissionList;

public record SavePermissionListCommand(SaveUserRolePermissionList permissionList) : IRequest<ResponseDTO<string>>
{
}

public class SavePermissionListCommandHandler : IRequestHandler<SavePermissionListCommand, ResponseDTO<string>>
{
    private readonly IPermissionRepository _permissionRepository;
    public SavePermissionListCommandHandler(IPermissionRepository permissionRepository)
    {
        _permissionRepository = permissionRepository;
    }

    public async Task<ResponseDTO<string>> Handle(SavePermissionListCommand command, CancellationToken cancellationToken)
    {
        var result = await _permissionRepository.SavePermissionList(command.permissionList, cancellationToken);

        if (string.IsNullOrWhiteSpace(result))
        {
            return ResponseDTO<string>.Failure(
                "Permissions not saved successfully",
                (int)HttpStatusCode.BadRequest
            );
        }

        return ResponseDTO<string>.Success(
            result,
            (int)HttpStatusCode.Created
        );
    }
}
