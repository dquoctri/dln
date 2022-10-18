using Context.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Uzer.Entity;

namespace Uzer.Context
{
    public class UserContext : DbContext, IDeadline
    {
        public static string SCHEMA = "dln_uzer";

        public DbSet<Partner>? Partners { get; set; }
        public DbSet<Organisation>? Organisations { get; set; }
        public DbSet<User>? Users { get; set; }

        public UserContext() {}

        public UserContext(DbContextOptions<UserContext> options) : base(options) {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var cs = $"Server=localhost,1433;Database={SCHEMA};User Id=admin;Password=P@ssword;";
                optionsBuilder.UseSqlServer(cs, x => x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, SCHEMA));
            }
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

        public override int SaveChanges()
        {
            throw new DbUpdateException("The context is read-only");
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new DbUpdateException("The context is read-only");
        }

        public int Deadline()
        {
            return base.SaveChanges();
        }

        public async Task<int> DeadlineAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
