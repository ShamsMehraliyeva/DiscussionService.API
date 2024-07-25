using Application.Utilities.JWT;
using Domain.Entities.Auth;

namespace Application.Features.Commands.Register;

public class RegisteredCommandResponse
{
    public AccessTokenModel AccessToken { get; set; }
    public RefreshTokenModel RefreshToken { get; set; }
}
