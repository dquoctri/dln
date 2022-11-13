using Authentication.Model;
using Repository.Common;

namespace Authentication.Repository
{
    public interface IUserRepository : ICrudRepository<User>
    {
    }
}
