using MediatR;
using NiotechoneCQRS.Application.DTOs.ResponseDTOs;
using NiotechoneCQRS.Domain.Interfaces;
using NiotechoneCQRS.Utility.AppResource;

namespace NiotechoneCQRS.Application.UserRole.Queries.GetAllRoles;

public class GetAllRolesQuery : IRequest<ResponseDTO<IList<Domain.Entities.UserRole>>>
{
}

public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, ResponseDTO<IList<Domain.Entities.UserRole>>>
{
    private readonly IUserRoleRepository _userRoleRepository;
    public GetAllRolesQueryHandler(IUserRoleRepository userRoleRepository)
    {
        _userRoleRepository = userRoleRepository;
    }

    public async Task<ResponseDTO<IList<Domain.Entities.UserRole>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _userRoleRepository
            .GetAllUserRoles(cancellationToken);

        if (roles == null || !roles.Any())
        {
            return ResponseDTO<IList<Domain.Entities.UserRole>>.Failure(
                string.Format(AppResource.Notfound, "Roles"),
                404
            );
        }

        return ResponseDTO<IList<Domain.Entities.UserRole>>.Success(roles, 200);
    }
}
