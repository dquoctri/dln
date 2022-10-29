using Authentication.Api.Models;
using Authentication.Repository;
using Authentication.Repository;

namespace Authentication.Api.Services
{
    public interface IUnitOfWork
    {
        IPartnerRepository Partners { get; }
        IOrganisationRepository Organisations { get; }
        IUserRepository Users { get; }
        IAccountRepository Accounts { get; }

        public List<PartitionTable> GetPartitionTable();

        Task<int> DeadlineAsync();
    }
}
