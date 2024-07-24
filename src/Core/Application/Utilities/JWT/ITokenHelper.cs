using Domain.Entities.Auth;

namespace Application.Utilities.JWT;

public interface ITokenHelper
{
    AccessToken CreateToken(User user);

    RefreshToken CreateRefreshToken(User user);
}
