using Microsoft.EntityFrameworkCore;

namespace Context.Common
{
    public abstract class ContextAware<T> where T : DbContext, new()
    {
        protected IContextFactory<T> ContextFactory { get; }

        protected virtual T CreateContext() => ContextFactory.CreateContext();

        public ContextAware()
        {
            ContextFactory = new ContextFactory<T>();
        }

        public ContextAware(IContextFactory<T> contextFactory)
        {
            ContextFactory = contextFactory ?? new ContextFactory<T>();
        }
    }
}
