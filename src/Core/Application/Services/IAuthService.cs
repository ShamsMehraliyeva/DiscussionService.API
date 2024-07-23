using Domain.Entities.Auth;
using Domain.Models.Auth;

namespace Application.Services
{
    public interface IAuthService
    {
        public Task<AccessTokenModel> CreateAccessToken(User user);
        public Task<RefreshToken> CreateRefreshToken(User user, string ipAddress);
        public Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken);
    }
}
