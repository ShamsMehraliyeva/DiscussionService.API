using System.Linq.Expressions;
using Domain.Entities;

namespace Application.Repositories.Abstractions
{
    public interface IRepository<T>
           where T : BaseEntity
    {
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetAsync(int id, CancellationToken cancellationToken);
        IQueryable<T> GetAll(bool noTracking = false);
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false);
        T Get(int id);
        bool Delete(int id);
        bool Delete(T entity);
        bool DeleteRange(ICollection<T> entities);
        void Update(T entity);
        T Add(T entity);
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(ICollection<T> entities);
    }
}
