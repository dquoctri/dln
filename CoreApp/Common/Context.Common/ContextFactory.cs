using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public T? Create(params object[] arguments)
        {
            if (InMemory)
            {
                var type = typeof(T);
                var o = Options ?? (Options = new DbContextOptionsBuilder<T>().UseInMemoryDatabase(databaseName: type.Name).Options);
                var newArguments = arguments.Prepend(o).ToArray();
                return Activator.CreateInstance(type, newArguments) as T;
            }
            return Activator.CreateInstance(typeof(T), arguments) as T;
        }
    }
}
