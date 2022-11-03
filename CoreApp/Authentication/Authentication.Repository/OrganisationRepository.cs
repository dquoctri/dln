using Authentication.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Common;

namespace Authentication.Repository
{
    public interface IOrganisationRepository : IRepository<Organizer>
    {
    }

    public class OrganisationRepository : Repository<Organizer>, IOrganisationRepository
    {
        public OrganisationRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
