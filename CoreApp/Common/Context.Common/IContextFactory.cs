using Microsoft.EntityFrameworkCore;

namespace Context.Common
{
    public interface IContextFactory<T> where T : DbContext, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        T CreateContext(params object[] arguments);
    }
}
