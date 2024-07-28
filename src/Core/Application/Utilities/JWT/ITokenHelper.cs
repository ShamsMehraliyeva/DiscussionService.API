using Domain.Entities;

namespace Application.Utilities.JWT;

public interface ITokenHelper
{
    AccessTokenModel CreateToken(User user);

    RefreshToken CreateRefreshToken(User user);
}
