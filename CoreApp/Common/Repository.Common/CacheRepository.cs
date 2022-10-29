using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Common
{
    public class CacheRepository<T> : IRepository<T> where T : class, new()
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IDistributedCache _cache;
        protected readonly DbContext _dbContext;
        protected internal DbSet<T> _dbSet;
        public CacheRepository(DbContext dbContext, IMemoryCache memoryCache, IDistributedCache cache)
        {
            _memoryCache = memoryCache;
            _cache = cache;
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public void Delete(params object?[]? keyValues)
        {
            RemoveFromCache(keyValues);

        }

        public void Delete(T entityToDelete)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Get(System.Linq.Expressions.Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public T? GetByID(params object?[]? keyValues)
        {
            var cachedT = _cache.Get(GetCacheKey(keyValues));
            if (cachedT != null)
            {
                var serializedT = Encoding.UTF8.GetString(cachedT);
                var t = JsonConvert.DeserializeObject<T>(serializedT);
                return t;
            }
            else
            {
                var t = _dbSet.Find(keyValues);
                if (t == null)
                {
                    return null;
                }
                var serializedT = JsonConvert.SerializeObject(t);
                cachedT = Encoding.UTF8.GetBytes(serializedT);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                _cache.Set(GetCacheKey(keyValues), cachedT, options);
                return t;
            }
        }

        public void Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        private T PutInCache(T entity, params object?[]? keyValues)
        {
            return _memoryCache.Set(GetCacheKey(keyValues), entity);
        }

        private void RemoveFromCache(params object?[]? keyValues)
        {
            _memoryCache.Remove(GetCacheKey(keyValues));
        }

        private T UpdateInCache(T entity, params object?[]? keyValues)
        {
            RemoveFromCache(keyValues);
            return PutInCache(entity, keyValues);
        }


        private string GetCacheKey(params object?[]? keyValues)
        {
            return typeof(T).FullName + keyValues;
        }
    }
}
