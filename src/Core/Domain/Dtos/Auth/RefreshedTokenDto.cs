using Domain.Entities.Auth;
using Domain.Models.Auth;

namespace Domain.Dtos.Auth;

public class RefreshedTokenDto
{
    public AccessTokenModel AccessToken { get; set; }
    public RefreshToken RefreshToken { get; set; }
}
