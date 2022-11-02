using Authentication.Repository;

namespace Authentication.Api.Services
{
    public interface IUnitOfWork
    {
        int Deadline();
        Task<int> DeadlineAsync();
    }
}
