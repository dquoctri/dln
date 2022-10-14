using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uzer.Repository
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        Task<int> DeadlineAsync();
    }
}
