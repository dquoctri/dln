using Authentication.Repository;

namespace Authentication.Api.Services
{
    public interface IUnitOfWork
    {
        IPartnerRepository Partners { get; }
        IOrganisationRepository Organisations { get; }
        IUserRepository Users { get; }
        IAccountRepository Accounts { get; }
        IProfileRepository Profiles { get; }
        Task<int> DeadlineAsync();
    }
}
