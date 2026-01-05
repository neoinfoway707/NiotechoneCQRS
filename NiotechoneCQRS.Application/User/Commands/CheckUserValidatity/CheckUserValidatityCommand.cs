using MediatR;
using NiotechoneCQRS.Application.DTOs.RequestDTOs;
using NiotechoneCQRS.Application.DTOs.ResponseDTOs;
using NiotechoneCQRS.Domain.Interfaces;
using NiotechoneCQRS.Utility.CommonResource;
using System.Net;

namespace NiotechoneCQRS.Application.User.Commands.CheckUserValidatity;

public record CheckUserValidatityCommand(LoginRequestDTO login) : IRequest<ResponseDTO<Domain.Entities.User>>
{
}

public class CheckUserValidatityQueryHandler : IRequestHandler<CheckUserValidatityCommand, ResponseDTO<Domain.Entities.User>>
{
    private readonly IUserRepository _userRepository;

    public CheckUserValidatityQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ResponseDTO<Domain.Entities.User>> Handle(CheckUserValidatityCommand request, CancellationToken cancellationToken)
    {

        var user = await _userRepository.IsUserValid(request.login.Email, request.login.Password,request.login.CompanyId);

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
