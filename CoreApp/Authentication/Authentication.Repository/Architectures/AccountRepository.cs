using Authentication.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Common;

namespace Authentication.Repository.Architectures
{
    public class AccountRepository : CrudRepository<Account>, IAccountRepository
    {
        public AccountRepository(DbContext dbContext) : base(dbContext) { }

        public Account? GetAccountByUsername(string username)
        {
            return _dbContext.Set<Account>().FirstOrDefault(a => a.Username == username);
        }
    }
}
