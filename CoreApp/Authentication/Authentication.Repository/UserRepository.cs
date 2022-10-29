using Authentication.Context;
using Authentication.Entity;
using Repository.Common;

namespace Authentication.Repository
{
    public interface IUserRepository : IRepository<User>
    {

    }

    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AuthenticationContext context) : base(context)
        {

        }
    }
}
