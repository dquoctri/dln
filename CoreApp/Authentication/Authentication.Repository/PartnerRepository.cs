using Authentication.Context;
using Authentication.Entity;
using Repository.Common;

namespace Authentication.Repository
{
    public interface IPartnerRepository : IRepository<Partner>
    {
        public Partner? GetByName(string name);
    }

    public class PartnerRepository : Repository<Partner>, IPartnerRepository
    {
        public PartnerRepository(AuthenticationContext dbContext) : base(dbContext)
        {
        }

        public Partner? GetByName(string name)
        {
            return _dbSet.Where(x => x.Name == name).FirstOrDefault();
        }
    }
}
