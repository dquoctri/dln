using Authentication.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Model.Common.Converters;

namespace Authentication.Context
{
    public class AuthenticationContext : DbContext
    {
        public static string SCHEMA = "dln_auth";

        public DbSet<Partner>? Partners { get; set; }
        public DbSet<Organizer>? Organizers { get; set; }
        public DbSet<User>? Users { get; set; }
        public DbSet<User>? Accounts { get; set; }
        public DbSet<Profile>? Profiles { get; set; }

        public AuthenticationContext() { }

        public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //local database for rendering mirgation
                var cs = $"Server=localhost,51433;Database=dln_auth;User Id=sa;Password=StrongP@ssword;TrustServerCertificate=True;";
                optionsBuilder.UseSqlServer(cs, x => x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, SCHEMA));
            }
            //optionsBuilder.LogTo(Console.WriteLine);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SCHEMA);
            modelBuilder.Entity<Profile>()
                .Property(e => e.Permissions)
                .HasConversion(new EnumsToStringConverter<Permission>());

            modelBuilder.Entity<Partner>()
               .Property(b => b.CreatedAt)
               .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Organizer>()
                .Property(b => b.CreateAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Organizer>()
                .Property(b => b.Type)
                .HasConversion(new EnumToStringConverter<OrganizerType>());

            modelBuilder.Entity<Organizer>()
                .Property(b => b.Status)
                .HasConversion(new EnumToStringConverter<OrganizerStatus>());
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
