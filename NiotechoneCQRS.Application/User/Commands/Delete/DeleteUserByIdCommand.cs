using MediatR;
using NiotechoneCQRS.Application.DTOs.ResponseDTOs;
using NiotechoneCQRS.Domain.Interfaces;
using NiotechoneCQRS.Utility.CommonResource;
using System.Net;

namespace NiotechoneCQRS.Application.User.Commands.Delete;

public class DeleteUserByIdCommand : IRequest<ResponseDTO<bool>>
{
    public string Id { get; set; }
}

public class DeleteUserByIdCommandHandler : IRequestHandler<DeleteUserByIdCommand, ResponseDTO<bool>>
{
    private readonly IUserRepository _userRepository;
    public DeleteUserByIdCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ResponseDTO<bool>> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
    {
        var isDeleted = await _userRepository
        .DeleteUserByIdAsync(request.Id, cancellationToken);

        if (!isDeleted)
        {
            return ResponseDTO<bool>.Failure(
                string.Format(AppResource.Notfound, "User"),
                (int)HttpStatusCode.NotFound
            );
        }
        return ResponseDTO<bool>.Success(isDeleted, (int)HttpStatusCode.OK);
    }
}
