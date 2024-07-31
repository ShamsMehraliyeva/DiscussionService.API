using Application.Repositories.Abstractions;
using Application.Utilities.JWT;
using Domain.Entities;

namespace Application.Services;

public class AuthManager : IAuthService
{
    private readonly ITokenHelper _tokenHelper;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    public AuthManager(ITokenHelper tokenHelper, IRefreshTokenRepository refreshTokenRepository)
    {
        _tokenHelper = tokenHelper;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
    {
        RefreshToken addedRefreshToken = await _refreshTokenRepository.AddAsync(refreshToken);
        return addedRefreshToken;
    }

    public async Task<RefreshToken> UpdateRefreshToken(RefreshToken currentRefreshToken, RefreshToken newRefreshToken)
    {
        currentRefreshToken.ExpireDate = DateTime.UtcNow;
        await _refreshTokenRepository.UpdateAsync(currentRefreshToken);
        
        RefreshToken addedRefreshToken = await _refreshTokenRepository.AddAsync(newRefreshToken);
        return addedRefreshToken;
    }


    public async Task<AccessTokenModel> CreateAccessToken(User user)
    {
        AccessTokenModel accessTokenModel = _tokenHelper.CreateToken(user);
        return accessTokenModel;
    }

    public async Task<RefreshToken> CreateRefreshToken(User user)
    {
        RefreshToken refreshToken =  _tokenHelper.CreateRefreshToken(user);
        return await Task.FromResult(refreshToken);
    }
}
