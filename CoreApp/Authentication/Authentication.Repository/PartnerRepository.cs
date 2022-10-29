using Authentication.Context;
using Authentication.Entity;
using Repository.Common;

namespace Authentication.Repository
{
    public interface IPartnerRepository : IRepository<Partner>
    {
    }

    public class PartnerRepository : Repository<Partner>, IPartnerRepository
    {
        public PartnerRepository(AuthenticationContext dbContext) : base(dbContext)
        {
        }
    }
}
