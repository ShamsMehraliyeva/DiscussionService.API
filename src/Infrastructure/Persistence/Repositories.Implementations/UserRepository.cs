using Application.Repositories.Abstractions;
using Domain.Entities.Auth;
using Persistence.Contexts;

namespace Persistence.Repositories.Implementations;

public class UserRepository:Repository<User>,IUserRepository
{
    public UserRepository(BaseDbContext context) : base(context)
    {
    }
}