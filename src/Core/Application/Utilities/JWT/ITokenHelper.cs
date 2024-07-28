using Domain.Entities;

namespace Application.Utilities.JWT;

public interface ITokenHelper
{
    AccessTokenModel CreateToken(User user);

    RefreshToken CreateRefreshToken(User user);
    public string GetToken();
    string GetUserClaim(string token, string claimName);
}
