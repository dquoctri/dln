using Authentication.Entity;
using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Migrations;
//using Authentication.Entity;

namespace Authentication.Context
{
    public class AuthenticationContext : DbContext
    {
        public static string SCHEMA = "dln_auth";

        public DbSet<Partner>? Partners { get; set; }
        public DbSet<Organizer>? Organizers { get; set; }
        public DbSet<User>? Users { get; set; }
        public DbSet<Account>? Accounts { get; set; }
        public DbSet<Profile>? Profiles { get; set; }

        public AuthenticationContext() { }

        public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    var cs = $"Server=localhost,51433;Database={SCHEMA};User Id=sa;Password=StrongP@ssword;";
            //    optionsBuilder.UseSqlServer(cs, x => x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, SCHEMA));
            //}
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SCHEMA);
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public override async ValueTask DisposeAsync()
        {
            await base.DisposeAsync();
        }
    }
}
