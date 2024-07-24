using Application.Utilities.JWT;
using Domain.Entities.Auth;

namespace Application.Features.Commands.Register;

public class RegisteredCommandResponse
{
    public AccessToken AccessToken { get; set; }
    public RefreshToken RefreshToken { get; set; }
}
