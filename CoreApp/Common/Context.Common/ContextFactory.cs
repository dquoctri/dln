using Microsoft.EntityFrameworkCore;

namespace Context.Common
{
    public class ContextFactory<T> : IContextFactory<T> where T : DbContext, new()
    {
        public ContextFactory() {}

        public T CreateContext(params object[] arguments)
        {
            if (Activator.CreateInstance(typeof(T), arguments) is T context) return context;
            throw new ArgumentException($"{typeof(T)} is not a DbContext!");
        }
    }
}
