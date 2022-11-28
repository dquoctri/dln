using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repository.Common
{
    public abstract class ReadRepository<T> : IReadRepository<T> where T : class, new()
    {
        protected readonly DbContext _dbContext;

        public ReadRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual IQueryable<T> GetAll()
        {
            return _dbContext.Set<T>().AsNoTracking();
        }

        public virtual T? GetByID(params object?[]? keyValues)
        {
            return _dbContext.Set<T>().Find(keyValues);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                _dbContext.Dispose();
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
