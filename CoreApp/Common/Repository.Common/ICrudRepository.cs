using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Common
{
    public interface ICrudRepository<T> : IRepository<T> where T : class, new()
    {
        void Insert(T entity);
        void Delete(params object?[]? keyValues);
        void Delete(T entityToDelete);
        void Update(T entity);
    }
}
