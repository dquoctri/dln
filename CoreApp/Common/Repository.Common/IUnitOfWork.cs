namespace Repository.Common
{
    public interface IUnitOfWork
    {
        int Deadline();
        Task<int> DeadlineAsync();
    }
}
