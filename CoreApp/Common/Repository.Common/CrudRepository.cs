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
            _dbContext.Set<T>().Add(entity);
        }

        public virtual void Delete(T entityToDelete)
        {
            if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbContext.Set<T>().Attach(entityToDelete);
            }
            _dbContext.Set<T>().Remove(entityToDelete);
        }

        public virtual void Update(T entity)
        {
            _dbContext.Set<T>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
