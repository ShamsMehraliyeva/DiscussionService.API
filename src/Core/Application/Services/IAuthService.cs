using Application.Utilities.JWT;
using Domain.Entities.Auth;

namespace Application.Services
{
    public interface IAuthService
    {
        public Task<AccessToken> CreateAccessToken(User user);
        public Task<RefreshToken> CreateRefreshToken(User user);
        public Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken);
    }
}
