using Microsoft.EntityFrameworkCore;

namespace Context.Common
{
    public class ContextFactory<T> where T : DbContext, new()
    {
        public bool InMemory { get; set; }
        private DbContextOptions<T>? Options { get; set; }
        public ContextFactory(bool inMemory = false)
        {
            InMemory = inMemory;
        }

        public T CreateContext(params object[] arguments)
        {
            var type = typeof(T);
            if (InMemory)
            {
                var o = Options ??= new DbContextOptionsBuilder<T>().UseInMemoryDatabase(databaseName: type.Name).Options;
                var newArguments = arguments.Prepend(o).ToArray();
                if (Activator.CreateInstance(type, newArguments) is T memoryContext)
                {
                    return memoryContext;
                }
            }
            if (Activator.CreateInstance(type, arguments) is T context)
            {
                return context;
            }
            throw new ArgumentException($"{typeof(T)} is not a DbContext!");
        }

        public void EnsureCreated()
        {
            if (!InMemory)
            {
                throw new InvalidOperationException("EnsureCreated for testing only!");
            }
            using (var ctx = CreateContext())
            {
                _ = ctx?.Database.EnsureCreated();
            }
        }

        public void EnsureDeleted()
        {
            if (!InMemory)
            {
                throw new InvalidOperationException("EnsureDeleted for testing only!");
            }
            using (var ctx = CreateContext())
            {
                _ = ctx?.Database.EnsureDeleted();
            }
        }
    }
}
