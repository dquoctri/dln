using Authentication.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Common;

namespace Authentication.Repository
{
    public interface IProfileRepository : IRepository<Profile>
    {
    }

    public class ProfileRepository : Repository<Profile>, IProfileRepository
    {
        public ProfileRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
