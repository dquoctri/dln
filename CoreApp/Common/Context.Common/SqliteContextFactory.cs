using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Context.Common
{
    public class SqliteContextFactory<T> : IContextFactory<T>, IDisposable where T : DbContext, new()
    {
        private readonly SqliteConnection _connection;

        public SqliteContextFactory()
        {
            _connection = new SqliteConnection("Filename=:memory:");
        }

        public T CreateContext(params object[] arguments)
        {
            _connection.Open();
            var options = new DbContextOptionsBuilder<T>().UseSqlite(_connection).Options;
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
            _connection.Dispose();
        }
    }
}
