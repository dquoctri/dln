using Microsoft.EntityFrameworkCore;

namespace Repository.Common
{
    public abstract class CrudRepository<T> : ReadRepository<T>, ICrudRepository<T> where T : class, new()
    {
        protected CrudRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public virtual void Insert(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public virtual void Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Set<T>().Attach(entity);
            }
            _context.Set<T>().Remove(entity);
        }

        public virtual void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
