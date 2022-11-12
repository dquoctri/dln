using Authentication.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Common;

namespace Authentication.Repository.Architectures
{
    public class OrganizerRepository : CrudRepository<Organizer>, IOrganizerRepository
    {
        public OrganizerRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
