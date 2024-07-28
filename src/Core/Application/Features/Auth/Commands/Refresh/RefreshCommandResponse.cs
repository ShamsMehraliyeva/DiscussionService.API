using Application.Utilities.JWT;

namespace Application.Features.Auth.Commands.Refresh;

public class RefreshCommandResponse
{
    public AccessTokenModel AccessToken { get; set; }
    public RefreshTokenModel RefreshToken { get; set; }
}