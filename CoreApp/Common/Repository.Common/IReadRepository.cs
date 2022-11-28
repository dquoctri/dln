using System.Linq.Expressions;

namespace Repository.Common
{
    public interface IReadRepository<T> : IDisposable where T : class, new()
    {
        IQueryable<T> GetAll();
        T? GetByID(params object?[]? keyValues);
    }
}
