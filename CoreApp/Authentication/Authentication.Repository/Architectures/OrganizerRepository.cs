using Authentication.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Common;

namespace Authentication.Repository.Architectures
{
    public class OrganizerRepository : CrudRepository<Organizer>, IOrganizerRepository
    {
        public OrganizerRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public bool IsExistedName(int partnerId, string name)
        {
            return _dbContext.Set<Organizer>().Any(x => x.PartnerId == partnerId && x.Name == name);
        }
    }
}
