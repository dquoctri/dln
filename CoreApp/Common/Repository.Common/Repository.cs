using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repository.Common
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _dbContext;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(T entity) 
            => await _dbContext.Set<T>().AddAsync(entity);

        public async Task AddRangeAsync(IEnumerable<T> entities) 
            => await _dbContext.Set<T>().AddRangeAsync(entities);

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
            => await _dbContext.Set<T>().Where(expression).ToArrayAsync();

        public async Task<IEnumerable<T>> GetAllAsync()
            => await _dbContext.Set<T>().ToArrayAsync();

        public async Task<T?> GetByIdAsync(long ID)
            => await _dbContext.Set<T>().FindAsync(ID);

        public void Remove(T entity)
            => _dbContext.Set<T>().Remove(entity);

        public void RemoveRange(IEnumerable<T> entities)
            => _dbContext.Set<T>().RemoveRange(entities);
    }
}
