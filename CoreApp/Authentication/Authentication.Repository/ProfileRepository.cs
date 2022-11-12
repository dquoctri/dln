using Authentication.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Common;

namespace Authentication.Repository
{
    public interface IProfileRepository : ICrudRepository<Profile>
    {
    }

    public class ProfileRepository : CrudRepository<Profile>, IProfileRepository
    {
        public ProfileRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
