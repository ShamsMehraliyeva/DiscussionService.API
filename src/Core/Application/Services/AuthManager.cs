using Application.Contracts.Auth;
using Application.Helpers.Security.TokenHelper;
using Domain.Entities.Auth;

namespace Application.Services;

public class AuthManager : IAuthService
{
    private readonly ITokenHelper _tokenHelper;

    public AuthManager(ITokenHelper tokenHelper)
    {
        _tokenHelper = tokenHelper;
    }

    public Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
    {
        throw new NotImplementedException();
    }

    public Task<AccessToken> CreateAccessToken(User user)
    {
        throw new NotImplementedException();
    }

    public Task<RefreshToken> CreateRefreshToken(User user, string ipAddress)
    {
        throw new NotImplementedException();
    }
}
