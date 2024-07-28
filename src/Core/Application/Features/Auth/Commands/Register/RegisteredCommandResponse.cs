using Application.Utilities.JWT;

namespace Application.Features.Commands.Auth.Register;

public class RegisteredCommandResponse
{
    public AccessTokenModel AccessToken { get; set; }
    public RefreshTokenModel RefreshToken { get; set; }
}
