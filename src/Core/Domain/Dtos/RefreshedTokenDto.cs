using Domain.Entities.Auth;
using Domain.Models.Auth;

namespace Application.Features.Auth.Dtos;

public class RefreshedTokenDto
{
    public AccessTokenModel AccessToken { get; set; }
    public RefreshToken RefreshToken { get; set; }
}
