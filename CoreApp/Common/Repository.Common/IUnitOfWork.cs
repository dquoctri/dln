namespace Repository.Common
{
    public interface IUnitOfWork : IDisposable
    {
        int Deadline();
        Task<int> DeadlineAsync();
    }
}
