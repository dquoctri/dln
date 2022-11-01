using Microsoft.EntityFrameworkCore;

namespace Context.Common
{
    public interface IContextFactory<T> where T : DbContext, new()
    {
        T CreateContext(params object[] arguments);
    }
}
