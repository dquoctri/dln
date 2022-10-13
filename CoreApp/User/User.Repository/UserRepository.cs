using System.Linq.Expressions;
using User.Context;

namespace User.Repository
{
    public class UserRepository : Repository<Entity.User>, IUserRepository
    {
        public UserRepository(UserContext context) : base(context)
        {

        }
    }
}
