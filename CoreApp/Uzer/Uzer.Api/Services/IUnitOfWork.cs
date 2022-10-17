using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uzer.Repository
{
    public interface IUnitOfWork
    {
        IPartnerRepository Partners { get; }
        IOrganisationRepository Organisations { get; }
        IUserRepository Users { get; }
        Task<int> DeadlineAsync();
    }
}
