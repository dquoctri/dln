using Authentication.Context;
using Authentication.Entity;
using Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Repository
{
    public interface IProfileRepository : IRepository<Profile>
    {
    }

    public class ProfileRepository : Repository<Profile>, IProfileRepository
    {
        public ProfileRepository(AuthenticationContext dbContext) : base(dbContext)
        {
        }
    }
}
