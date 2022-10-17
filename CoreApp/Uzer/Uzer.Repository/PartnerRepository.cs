using Repository.Common;
using Uzer.Context;
using Uzer.Entity;

namespace Uzer.Repository
{
    public class PartnerRepository : Repository<Partner>, IPartnerRepository
    {
        public PartnerRepository(UserContext dbContext) : base(dbContext)
        {
        }
    }
}
