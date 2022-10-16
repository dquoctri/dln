using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging;
using Uzer.Entity;

namespace Uzer.Context
{
    public class UserContext : DbContext
    {
        public static string SCHEMA = "dln_uzer";

        private readonly StreamWriter _logStream = new StreamWriter("mylog.txt", append: true);

        public DbSet<Partner>? Partners { get; set; }
        public DbSet<Organisation>? Organisations { get; set; }
        public DbSet<User>? Users { get; set; }

        public UserContext()
        {
        }

        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var cs = $"Server=localhost,1433;Database={SCHEMA};User Id=admin;Password=P@ssword;";
                optionsBuilder.UseSqlServer(cs, x => x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, SCHEMA));
            }
            //view more https://learn.microsoft.com/en-us/ef/core/logging-events-diagnostics/simple-logging
            optionsBuilder.LogTo(_logStream.WriteLine, LogLevel.Information).EnableDetailedErrors();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SCHEMA);
        }

        public override void Dispose()
        {
            base.Dispose();
            _logStream.Dispose();
        }

        public override async ValueTask DisposeAsync()
        {
            await base.DisposeAsync();
            await _logStream.DisposeAsync();
        }
    }
}
