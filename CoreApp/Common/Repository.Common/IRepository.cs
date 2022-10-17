using System.Linq.Expressions;

namespace Repository.Common
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Get(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "");
        T? GetByID(params object?[]? keyValues);
        void Insert(T entity);
        void Delete(params object?[]? keyValues);
        void Delete(T entityToDelete);
        void Update(T entity);
    }
}
