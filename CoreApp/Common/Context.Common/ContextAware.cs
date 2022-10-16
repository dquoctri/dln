using Microsoft.EntityFrameworkCore;

namespace Context.Common
{
    public abstract class ContextAware<T> where T : DbContext, new()
    {
        protected ContextFactory<T> ContextFactory { get; }
        protected virtual T Context() => ContextFactory.Create();

        public ContextAware() : this(null)
        { }

        public ContextAware(ContextFactory<T>? contextFactory)
        {
            ContextFactory = contextFactory ?? new ContextFactory<T>();
        }
    }
}
