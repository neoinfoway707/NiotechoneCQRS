using MediatR;
using NiotechoneCQRS.Domain.Interfaces;

namespace NiotechoneCQRS.Application.Login.Update;

public class UpdateUserTokenCommand : IRequest<bool>
{
    public long UserId { get; set; }
    public string JwtToken { get; set; } = string.Empty;
    public DateTime TokenExpiry { get; set; }
}

public class UpdateUserTokenCommandHandler : IRequestHandler<UpdateUserTokenCommand, bool>
{
    private readonly IUserRepository _userRepository;
    public UpdateUserTokenCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<bool> Handle(UpdateUserTokenCommand request, CancellationToken cancellationToken)
    {
        return await _userRepository.UpdateUserTokenAsync(request.UserId, request.JwtToken, cancellationToken);
    }
}