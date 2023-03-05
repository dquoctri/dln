using Authentication.Model;
using Repository.Common;

namespace Authentication.Repository
{
    public interface IUserRepository : ICrudRepository<User>
    {
        public User? GetAccountByUsername(string username);

        public bool IsExistedUsername(string username);
    }
}
