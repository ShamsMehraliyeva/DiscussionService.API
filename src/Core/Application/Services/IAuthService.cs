using Application.Utilities.JWT;
using Domain.Entities;

namespace Application.Services
{
    public interface IAuthService
    {
        public Task<AccessTokenModel> CreateAccessToken(User user);
        public Task<RefreshToken> CreateRefreshToken(User user);
        public Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken);
    }
}
