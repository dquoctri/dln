using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Common
{
    public abstract class CrudRepository<T> : Repository<T>, ICrudRepository<T> where T : class, new()
    {
        protected CrudRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public virtual void Insert(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public virtual void Delete(params object?[]? keyValues)
        {
            T? entity = _dbContext.Set<T>().Find(keyValues);
            if (entity != null)
            {
                Delete(entity);
            }
        }

        public virtual void Delete(T entityToDelete)
        {
            // TODO: shuold checking behavior
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
