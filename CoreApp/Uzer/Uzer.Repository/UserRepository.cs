using Repository.Common;
using Uzer.Context;
using Uzer.Entity;

namespace Uzer.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(UserContext context) : base(context)
        {

        }
    }
}
