using AutoMapper;
using MediatR;
using NiotechoneCQRS.Application.DTOs.ResponseDTOs;
using NiotechoneCQRS.Domain.Interfaces;
using System.ComponentModel;
using System.Net;

namespace NiotechoneCQRS.Application.User.Commands.Create;

public class CreateUserCommand : IRequest<ResponseDTO<bool>>
{
    public string FullName { get; set; } = string.Empty;
    public int? UserRoleId { get; set; }
    public int? CompanyId { get; set; }
    public string Address { get; set; } = string.Empty;
    public int? CountryId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    [DefaultValue(1)]
    public int StatusId { get; set; } = 1;
    public int UserTypeId { get; set; } = 1;
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ResponseDTO<bool>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public CreateUserCommandHandler(IUserRepository userRepository,IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public async Task<ResponseDTO<bool>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<Domain.Entities.User>(request);
        var result = await _userRepository.CreateUserAsync(user, cancellationToken);
        if (!result)
        {
            return ResponseDTO<bool>.Failure(
                "User creation failed",
                (int)HttpStatusCode.BadRequest
            );
        }

        return ResponseDTO<bool>.Success(true, (int)HttpStatusCode.Created);
    }
}