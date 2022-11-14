using Authentication.Model;
using Repository.Common;

namespace Authentication.Repository
{
    public interface IPartnerRepository : ICrudRepository<Partner>
    {
        public Partner? GetByName(string name);
        public bool IsExistedName(string name);
    }
}
