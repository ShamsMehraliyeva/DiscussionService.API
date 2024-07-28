using Application.Utilities.JWT;

namespace Application.Features.Commands.Auth.Login;

public class LoginCommandResponse
{
    public AccessTokenModel AccessToken { get; set; }
    public RefreshTokenModel RefreshToken { get; set; }
}