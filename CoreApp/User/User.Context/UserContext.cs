using Microsoft.EntityFrameworkCore;
using User.Entity;

namespace User.Context
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<Entity.User> Users { get; set; }
    }
}
