using Microsoft.EntityFrameworkCore;

namespace Repository.Common
{
    public abstract class ReadRepository<T> : IReadRepository<T> where T : class, new()
    {
        protected readonly DbContext _context;

        public ReadRepository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException($"{typeof(DbContext)} must not be null!");
        }

        public virtual IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public virtual T? GetByID(params object?[]? keyValues)
        {
            return _context.Set<T>().Find(keyValues);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                _context.Dispose();
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
