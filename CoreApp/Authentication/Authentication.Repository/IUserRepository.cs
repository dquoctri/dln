using Authentication.Entity;
using Repository.Common;

namespace Authentication.Repository
{
    public interface IUserRepository : ICrudRepository<User>
    {
    }
}
