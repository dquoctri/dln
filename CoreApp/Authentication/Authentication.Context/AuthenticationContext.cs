﻿using Authentication.Entity;
using Authentication.Entity.Converters;
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
                var cs = $"Server=localhost,51433;Database=dln_auth;User Id=sa;Password=StrongP@ssword;";
                optionsBuilder.UseSqlServer(cs, x => x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, SCHEMA));
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var valueComparer = new ValueComparer<ISet<UserRole>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => (ISet<UserRole>)c.ToHashSet());

            var valueConversion = new EnumCollectionJsonValueConverter<UserRole>();

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
               .HasDefaultValueSql("getutcdate()");

            modelBuilder.Entity<Organizer>()
                .Property(b => b.CreatedDate)
                .HasDefaultValueSql("getutcdate()");
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
