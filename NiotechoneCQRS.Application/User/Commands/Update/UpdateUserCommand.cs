using AutoMapper;
using MediatR;
using NiotechoneCQRS.Application.DTOs.ResponseDTOs;
using NiotechoneCQRS.Domain.Interfaces;
using System.Net;
using System.Text.Json.Serialization;

namespace NiotechoneCQRS.Application.User.Commands.Update;

public class UpdateUserCommand : IRequest<ResponseDTO<bool>>
{
    [JsonIgnore]         
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public int UserRoleId { get; set; }
    public int? CompanyId { get; set; }
    public string Address { get; set; } = string.Empty;
    public int CountryId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    //public string? Password { get; set; }
    public int StatusId { get; set; }
}
public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ResponseDTO<bool>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public UpdateUserCommandHandler(IUserRepository userRepository,IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public async Task<ResponseDTO<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<Domain.Entities.User>(request);
        var updated = await _userRepository
            .UpdateUserAsync(user, cancellationToken);

        if (!updated)
        {
            return ResponseDTO<bool>.Failure(
                "User not found or update failed",
                (int)HttpStatusCode.NotFound
            );
        }

        return ResponseDTO<bool>.Success(
            true,
            (int)HttpStatusCode.OK
        );
    }
}