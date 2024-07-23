using Application.Contracts.Auth;
using Domain.Entities.Auth;

namespace Application.Helpers.Security.TokenHelper;

public interface ITokenHelper
{
    AccessToken CreateToken(User user);

    RefreshToken CreateRefreshToken(User user, string ipAddress);
}
