using System.Linq.Expressions;
using Domain.Entities;

namespace Application.Repositories.Abstractions
{
    public interface IRepository<T>
           where T : BaseEntity
    {
        IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetAsync(int id, CancellationToken cancellationToken);
        IQueryable<T> GetAll(bool noTracking = false);
        T Get(int id);
        bool Delete(int id);
        bool Delete(T entity);
        bool DeleteRange(ICollection<T> entities);
        void Update(T entity);
        T Add(T entity);
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(ICollection<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(IEnumerable<T> entities);

    }
}
