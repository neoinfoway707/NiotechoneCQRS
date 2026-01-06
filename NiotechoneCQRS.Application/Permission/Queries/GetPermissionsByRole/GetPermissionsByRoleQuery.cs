using MediatR;
using NiotechoneCQRS.Application.DTOs.ResponseDTOs;
using NiotechoneCQRS.Domain.Interfaces;
using NiotechoneCQRS.Utility.AppResource;
using System.Net;

namespace NiotechoneCQRS.Application.Permission.Queries.GetPermissionsByRole;

public class GetPermissionsByRoleQuery : IRequest<ResponseDTO<List<Domain.Entities.UserRoleModuleOperation>>>
{
    public int RoleId { get; set; }
}

public class GetPermissionsByRoleQueryHandler : IRequestHandler<GetPermissionsByRoleQuery, ResponseDTO<List<Domain.Entities.UserRoleModuleOperation>>>
{
    private readonly IPermissionRepository _permissionRepository;
    public GetPermissionsByRoleQueryHandler(IPermissionRepository permissionRepository)
    {
        _permissionRepository = permissionRepository;
    }

    public async Task<ResponseDTO<List<Domain.Entities.UserRoleModuleOperation>>> Handle(GetPermissionsByRoleQuery request, CancellationToken cancellationToken)
    {
        var permissions = await _permissionRepository.GetPermissionList(request.RoleId, cancellationToken);
        if (permissions == null)
        {
            return ResponseDTO<List<Domain.Entities.UserRoleModuleOperation>>.Failure(
                string.Format(AppResource.Notfound, "Permissions"),
                (int)HttpStatusCode.NotFound
            );
        }

        return ResponseDTO<List<Domain.Entities.UserRoleModuleOperation>>.Success(permissions, (int)HttpStatusCode.OK);
    }
}
