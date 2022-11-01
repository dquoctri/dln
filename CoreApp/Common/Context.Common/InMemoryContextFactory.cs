using Microsoft.EntityFrameworkCore;

namespace Context.Common
{
    public class InMemoryContextFactory<T> : IContextFactory<T>, IDisposable where T : DbContext, new()
    {
        public T CreateContext(params object[] arguments)
        {
            var options = new DbContextOptionsBuilder<T>().UseInMemoryDatabase(databaseName: typeof(T).Name).Options;
            var newArguments = arguments.Prepend(options).ToArray();
            if (Activator.CreateInstance(typeof(T), newArguments) is T context)
            {
                context.Database.EnsureCreated();
                return context;
            }
            throw new ArgumentException($"{typeof(T)} is not a DbContext!");
        }

        public void EnsureDeleted()
        {
            using (T context = CreateContext())
            {
                context.Database.EnsureDeleted();
            }
        }

        public void Dispose()
        {
            EnsureDeleted();
        }
    }
}
