using Domain.Entities.Auth;

namespace Application.Repositories.Abstractions;

public interface IUserRepository: IRepository<User>
{
    Task<User> GetByRefreshTokenAsync(string refreshToken);
}