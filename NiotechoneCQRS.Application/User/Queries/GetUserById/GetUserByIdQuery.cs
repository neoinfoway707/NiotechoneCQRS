using MediatR;
using NiotechoneCQRS.Application.DTOs.ResponseDTOs;
using NiotechoneCQRS.Domain.Interfaces;
using NiotechoneCQRS.Utility.AppResource;
using System.Net;

namespace NiotechoneCQRS.Application.User.Queries.GetUserById;

public class GetUserByIdQuery : IRequest<ResponseDTO<Domain.Entities.User>>
{
    public string Id { get; set; }
}

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ResponseDTO<Domain.Entities.User>>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ResponseDTO<Domain.Entities.User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {

        var user = await _userRepository.GetUserByIdAsync(request.Id, cancellationToken);
        if (user == null)
        {
            return ResponseDTO<Domain.Entities.User>.Failure(
                string.Format(AppResource.Notfound, "User"),
                (int)HttpStatusCode.NotFound
            );
        }

        return ResponseDTO<Domain.Entities.User>.Success(user, (int)HttpStatusCode.OK);
    }
}

