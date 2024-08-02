using Application.Repositories.Abstractions;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.Implementations;

public class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
{
    private readonly BaseDbContext _context;
    public RefreshTokenRepository(BaseDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<RefreshToken> UpdateRefreshTokenTransaction(RefreshToken newRefreshToken)
    {
        
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                var activeUserTokens = this.GetWhere(x=>
                    x.UserId == newRefreshToken.UserId 
                    && x.ExpireDate>=DateTime.UtcNow).ToList();
                
                activeUserTokens = activeUserTokens.Select(token => 
                {
                    token.ExpireDate = DateTime.UtcNow;
                    return token;
                }).ToList();
                
                await this.UpdateRangeAsync(activeUserTokens);
                
                RefreshToken addedRefreshToken = await this.AddAsync(newRefreshToken);
                
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return addedRefreshToken;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception(ex.Message);
            }
        }
    }
}
