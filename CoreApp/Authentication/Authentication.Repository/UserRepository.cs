using Authentication.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Common;

namespace Authentication.Repository
{
    public interface IUserRepository : IRepository<User>
    {

    }

    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {

        }
    }
}
