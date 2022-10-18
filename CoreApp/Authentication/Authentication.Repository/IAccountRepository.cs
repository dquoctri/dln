using Authentication.Entity;
using Repository.Common;

namespace Authentication.Repository
{
    public interface IAccountRepository : IRepository<Account>
    {
        public Account? GetAccountByUsername(string username);
    }
}
