using Authentication.Model;
using Authentication.Model.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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
#pragma warning disable CS8604 // Possible null reference argument for parameter 'second' in 'bool Enumerable.SequenceEqual<UserRole>(IEnumerable<UserRole> first, IEnumerable<UserRole> second)'.
#pragma warning disable CS8604 // Possible null reference argument for parameter 'first' in 'bool Enumerable.SequenceEqual<UserRole>(IEnumerable<UserRole> first, IEnumerable<UserRole> second)'.
            var valueComparer = new ValueComparer<ISet<UserRole>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => (ISet<UserRole>)c.ToHashSet());
#pragma warning restore CS8604 // Possible null reference argument for parameter 'first' in 'bool Enumerable.SequenceEqual<UserRole>(IEnumerable<UserRole> first, IEnumerable<UserRole> second)'.
#pragma warning restore CS8604 // Possible null reference argument for parameter 'second' in 'bool Enumerable.SequenceEqual<UserRole>(IEnumerable<UserRole> first, IEnumerable<UserRole> second)'.

            var valueConversion = EnumCollectionJsonValueConverter3<UserRole>.CreateConverter();

            modelBuilder.HasDefaultSchema(SCHEMA);
            modelBuilder.Entity<Profile>()
                .Property(e => e.Roles)
                .HasConversion(valueConversion);
            //.HasConversion(
            //    v => string.Join(',', v),
            //    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => (UserRole)Enum.Parse(typeof(UserRole), x)).ToList() ?? new List<UserRole>())
            //.Metadata.SetValueComparer(valueComparer);

            modelBuilder.Entity<Partner>()
               .Property(b => b.CreatedDate)
               .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Organizer>()
                .Property(b => b.CreatedDate)
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
