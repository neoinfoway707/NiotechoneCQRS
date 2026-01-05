using MediatR;
using Microsoft.AspNetCore.Http;
using NiotechoneCQRS.Application.DTOs.ResponseDTOs;
using NiotechoneCQRS.Domain.Interfaces;
using NiotechoneCQRS.Utility.CommonResource;

namespace NiotechoneCQRS.Application.User.Queries.GetAllUsers;

public record GetAllUsersQuery: IRequest<ResponseDTO<IList<Domain.Entities.User>>>
{
}
public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, ResponseDTO<IList<Domain.Entities.User>>>
{
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetAllUsersQueryHandler(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ResponseDTO<IList<Domain.Entities.User>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository
            .GetAllUsers(cancellationToken);

        if (users == null || !users.Any())
        {
            return ResponseDTO<IList<Domain.Entities.User>>.Failure(
                string.Format(AppResource.Notfound, "Users"),
                404
            );
        }

        return ResponseDTO<IList<Domain.Entities.User>>.Success(users, 200);
    }
}
