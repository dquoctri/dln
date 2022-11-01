using Authentication.Context;
using Authentication.Entity;
using Repository.Common;

namespace Authentication.Repository
{
    public interface IPartnerRepository : IRepository<Partner>
    {
        public Partner? GetByName(string name);
        public bool IsExisted(string name);
    }

    public class PartnerRepository : Repository<Partner>, IPartnerRepository
    {
        public PartnerRepository(AuthenticationContext dbContext) : base(dbContext)
        {
        }

        public Partner? GetByName(string name)
        {
            return _dbContext.Set<Partner>().Where(x => x.Name == name).FirstOrDefault();
        }

        public bool IsExisted(string name)
        {
            return _dbContext.Set<Partner>().Any(x => x.Name.Equals(name));
        }
    }
}
