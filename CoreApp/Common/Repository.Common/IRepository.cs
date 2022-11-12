using System.Linq.Expressions;

namespace Repository.Common
{
    public interface IRepository<T> where T : class, new()
    {
        IEnumerable<T> FindAll(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "");
        T? FindByID(params object?[]? keyValues);
    }
}
