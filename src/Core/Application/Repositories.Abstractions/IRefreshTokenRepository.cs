using Domain.Entities;

namespace Application.Repositories.Abstractions
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
        Task<RefreshToken> UpdateRefreshTokenTransaction(RefreshToken token);
    }
}
