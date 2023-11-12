using Authentication.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Common;

namespace Authentication.Repository.Architectures
{
    public class UserRepository : CrudRepository<User>, IUserRepository
    {
        public UserRepository(DbContext dbContext) : base(dbContext) { }

        public User? GetAccountByEmail(string email)
        {
            return _context.Set<User>().FirstOrDefault(a => a.Email == email);
        }

        public bool IsExistedEmail(string email)
        {
            return _context.Set<User>().Any(a => a.Email == email);
        }
    }
}
