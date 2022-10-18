using Authentication.Repository;

namespace Authentication.Api.Services
{
    public interface IUnitOfWork
    {
        IAccountRepository Accounts { get; }

        Task<int> DeadlineAsync();
    }
}
