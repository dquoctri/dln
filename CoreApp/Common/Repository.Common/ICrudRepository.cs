﻿namespace Repository.Common
{
    public interface ICrudRepository<T> : IReadRepository<T> where T : class, new()
    {
        void Insert(T entity);
        void Delete(T entityToDelete);
        void Update(T entity);
    }
}
