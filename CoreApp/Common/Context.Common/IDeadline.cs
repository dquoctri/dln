using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context.Common
{
    public interface IDeadline
    {
        int Deadline();
        Task<int> DeadlineAsync(CancellationToken cancellationToken = default);
    }
}
