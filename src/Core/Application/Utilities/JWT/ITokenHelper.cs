using Domain.Entities.Auth;

namespace Application.Utilities.JWT;

public interface ITokenHelper
{
    AccessTokenModel CreateToken(User user);

    RefreshToken CreateRefreshToken(User user);
}
