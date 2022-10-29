using Authentication.Context;
using Authentication.Entity;
using Repository.Common;

namespace Authentication.Repository
{
    public interface IOrganisationRepository : IRepository<Organizer>
    {
    }

    public class OrganisationRepository : Repository<Organizer>, IOrganisationRepository
    {
        public OrganisationRepository(AuthenticationContext dbContext) : base(dbContext)
        {
        }
    }
}
