using Authentication.Model;
using Repository.Common;

namespace Authentication.Repository
{
    public interface IAccountRepository : ICrudRepository<Account>
    {
        public Account? GetAccountByUsername(string username);

        public bool IsExistedUsername(string username);
    }
}
