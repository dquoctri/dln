using Authentication.Entity;
using Repository.Common;

namespace Authentication.Repository
{
    public interface IPartnerRepository : ICrudRepository<Partner>
    {
        public Partner? GetByName(string name);
        public bool IsExisted(string name);
    }
}
