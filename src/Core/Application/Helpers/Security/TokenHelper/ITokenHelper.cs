using Domain.Entities.Auth;
using Domain.Models.Auth;

namespace Application.Helpers.Security.TokenHelper;

public interface ITokenHelper
{
    AccessTokenModel CreateToken(User user);

    RefreshToken CreateRefreshToken(User user, string ipAddress);
}
