using Repository.Common;
using Uzer.Context;
using Uzer.Entity;

namespace Uzer.Repository
{
    public class OrganisationRepository : Repository<Organisation>, IOrganisationRepository
    {
        public OrganisationRepository(UserContext dbContext) : base(dbContext)
        {
        }
    }
}
