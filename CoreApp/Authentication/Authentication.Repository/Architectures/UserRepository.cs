using Authentication.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Common;

namespace Authentication.Repository.Architectures
{
    public class UserRepository : CrudRepository<User>, IUserRepository
    {
        public UserRepository(DbContext dbContext) : base(dbContext) { }

        public User? GetAccountByUsername(string username)
        {
            return _context.Set<User>().FirstOrDefault(a => a.Username == username);
        }

        public bool IsExistedUsername(string username)
        {
            return _context.Set<User>().Any(a => a.Username == username);
        }
    }
}
