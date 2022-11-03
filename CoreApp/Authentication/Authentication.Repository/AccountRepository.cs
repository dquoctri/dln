using Authentication.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Common;

namespace Authentication.Repository
{
    public interface IAccountRepository : IRepository<Account>
    {
        public Account? GetAccountByUsername(string username);
    }

    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(DbContext dbContext) : base(dbContext) {}

        public Account? GetAccountByUsername(string username)
        {
            return _dbContext.Set<Account>().FirstOrDefault(a => a.Username == username);
        }
    }
}
