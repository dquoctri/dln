using System.Linq.Expressions;

namespace Repository.Common
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(long ID);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
