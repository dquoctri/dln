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
            return _context.Set<Account>().FirstOrDefault(a => a.Username == username);
        }

        public bool IsExistedUsername(string username)
        {
            return _context.Set<Account>().Any(a => a.Username == username);
        }
    }
}
