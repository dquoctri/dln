using Authentication.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Context
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AuthenticationContext(
                serviceProvider.GetRequiredService<DbContextOptions<AuthenticationContext>>()))
            {
               context.Database.Migrate();
                InitPartners(context);
                InitOrganizers(context);
                InitProfiles(context);
                InitAccounts(context);
            }
        }

        public static void InitPartners(AuthenticationContext context)
        {
            // Look for any partners.
            if (context.Partners == null || context.Partners.Any())
            {
                return;
            }
            context.Partners.AddRange(
                new Partner
                {
                    Name = "System",
                    Description = "The system partner"
                }
            );
            context.SaveChanges();
        }

        public static void InitOrganizers(AuthenticationContext context)
        {
            // Look for any partners.
            if (context.Partners == null || context.Organizers == null || context.Organizers.Any())
            {
                return;
            }
            var partner = context.Partners.Where(p => "System".Equals(p.Name)).FirstOrDefault();
            // No partner found
            if (partner == null)
            {
                return;
            };
            context.Organizers.AddRange(
                new Organizer
                {
                    Name = "System",
                    Description = "The system partner",
                    Partner = partner,
                    Type = OrganizerType.SYSTEM,
                }
            );
            context.SaveChanges();
        }

        public static void InitProfiles(AuthenticationContext context)
        {
            // Look for any profiles.
            if (context.Profiles == null || context.Profiles.Any())
            {
                return;   // DB has been seeded
            }
            context.Profiles.AddRange(new Profile
            {
                Name = "admin",
                Description = "admin profile",
                Permissions = new HashSet<Permission>() { Permission.PARTNER_MANAGER, Permission.ORGANIZER_MANAGER, Permission.PROFILE_MANAGER, Permission.USER_MANAGER }
            });

            context.SaveChanges();
        }

        public static void InitAccounts(AuthenticationContext context)
        {
            // Look for any accounts.
            if (context.Organizers == null || context.Accounts == null || context.Accounts.Any())
            {
                return;   // DB has been seeded
            }
            var organizer = context.Organizers.Where(p => "System".Equals(p.Name)).FirstOrDefault();
            // No organizer found
            if (organizer == null)
            {
                return;
            };
            context.Accounts.AddRange(
                new User
                {
                    Email = "admin@dln.com",
                    Hash = "123547",
                    Salt = "abc",
                    Organizer = organizer,
                },
                new User
                {
                    Email = "user@dln.com",
                    Hash = "123547",
                    Salt = "abc",
                    Organizer = organizer,
                }
            );
            context.SaveChanges();
        }
    }
}
