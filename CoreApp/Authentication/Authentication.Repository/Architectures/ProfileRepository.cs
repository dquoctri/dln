using Authentication.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Common;

namespace Authentication.Repository.Architectures
{
    public class ProfileRepository : CrudRepository<Profile>, IProfileRepository
    {
        public ProfileRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public bool IsExistedName(string name)
        {
            return _context.Set<Profile>().Any(p => p.Name == name);
        }
    }
}
