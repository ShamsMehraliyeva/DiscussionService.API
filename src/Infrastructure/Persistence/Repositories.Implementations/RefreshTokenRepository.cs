using Application.Repositories.Abstractions;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.Implementations;

public class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(BaseDbContext context) : base(context)
    {
    }
}
