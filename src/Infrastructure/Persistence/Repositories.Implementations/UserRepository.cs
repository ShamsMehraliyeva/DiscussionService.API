using Application.Repositories.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories.Implementations;

public class UserRepository:Repository<User>,IUserRepository
{
    private readonly BaseDbContext _context;
    public UserRepository(BaseDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<User> GetByRefreshTokenAsync(string refreshToken)
    {
        return await _context.Users
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.RefreshTokens.Any(r => r.Token == refreshToken));
    }
}