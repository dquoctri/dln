using Authentication.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Common;

namespace Authentication.Repository
{
    public interface IOrganisationRepository : ICrudRepository<Organizer>
    {
    }

    public class OrganisationRepository : CrudRepository<Organizer>, IOrganisationRepository
    {
        public OrganisationRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
