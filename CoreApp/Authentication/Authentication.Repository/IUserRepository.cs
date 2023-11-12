using Authentication.Model;
using Repository.Common;

namespace Authentication.Repository
{
    public interface IUserRepository : ICrudRepository<User>
    {
        public User? GetAccountByEmail(string email);

        public bool IsExistedEmail(string email);
    }
}
